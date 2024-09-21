using MiaTicket.Setting;
using MiaTicket.VNPay.Constant;
using MiaTicket.VNPay.Library;
using MiaTicket.VNPay.Util;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using MiaTicket.VNPay.Response;
using MiaTicket.VNPay.Model;

namespace MiaTicket.VNPay
{
    public class VNPayService
    {
        private static VNPayService _instance;
        private static object _lock = new object();
        private EnviromentSetting _setting = EnviromentSetting.GetInstance();
        private HttpClient _httpClient;
        private VNPayService()
        {

        }

        public static VNPayService GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new VNPayService();
                        _instance._httpClient = new HttpClient();
                    }
                    return _instance;
                }
            }
            return _instance;
        }

        public CreatePaymentResult CreatePayment(IHttpContextAccessor httpContextAccessor, double totalPrice)
        {
            var vnPayLibrary = new VNPayLibrary();
            var createdAt = DateTime.Now;
            var expireAt = createdAt.AddMinutes(VNPayConstant.VNPAY_EXPIRE_IN_MINUTE);

            var createdUTCAt = DateTime.UtcNow;
            var expireUTCAt = createdUTCAt.AddMinutes(VNPayConstant.VNPAY_EXPIRE_IN_MINUTE);

            string vnp_Returnurl = _setting.GetVnPayReturnUrl(); //URL nhan ket qua tra ve 
            string vnp_Url = VNPayConstant.VNPAY_URL; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = _setting.GetVnPayTMNCode(); //Ma website
            string vnp_HashSecret = _setting.GetVnPaySecretKey(); //Chuoi bi mat
            string vnp_Version = VNPayConstant.VNPAY_VERSION;
            var vnp_CreateDate = createdAt.ToString("yyyyMMddHHmmss");
            string vnp_IpAddr = VNPayUtil.GetIpAddress(httpContextAccessor);
            var vnp_Amount = (totalPrice * 100).ToString();
            var vnp_CurrCode = "VND";
            var vnp_OrderType = "other";
            var vnp_OrderInfo = "MiaTicket Order Payment";
            var vnp_TxnRef = VNPayUtil.GetRandomNumber(8);
            var vnp_ExpireDate = expireAt.ToString("yyyyMMddHHmmss");
            var vnp_Command = "pay";
            var vnp_Locale = "vn";

            vnPayLibrary.AddRequestData("vnp_Version", vnp_Version);
            vnPayLibrary.AddRequestData("vnp_Command", vnp_Command);
            vnPayLibrary.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnPayLibrary.AddRequestData("vnp_Amount", vnp_Amount); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            vnPayLibrary.AddRequestData("vnp_CreateDate", vnp_CreateDate);
            vnPayLibrary.AddRequestData("vnp_CurrCode", vnp_CurrCode);
            vnPayLibrary.AddRequestData("vnp_IpAddr", vnp_IpAddr);
            vnPayLibrary.AddRequestData("vnp_Locale", vnp_Locale);
            vnPayLibrary.AddRequestData("vnp_OrderInfo", vnp_OrderInfo);
            vnPayLibrary.AddRequestData("vnp_OrderType", vnp_OrderType); //default value: other
            vnPayLibrary.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnPayLibrary.AddRequestData("vnp_TxnRef", vnp_TxnRef); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            string paymentUrl = vnPayLibrary.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            CreatePaymentResult result = new CreatePaymentResult()
            {
                TransactionCode = vnp_TxnRef,
                CreatedAt = createdUTCAt,
                ExpireAt = expireUTCAt,
                TotalAmount = totalPrice,
                PaymentUrl = paymentUrl,
            };
            return result;
        }

        public async Task<QueryPaymentResult?> QueryPaymentAsync(IHttpContextAccessor httpContextAccessor, string transactionCode, string transactionDate)
        {
            var vnp_Api = VNPayConstant.VNPAY_API;
            var vnp_HashSecret = _setting.GetVnPaySecretKey(); //Secret KEy
            var vnp_TmnCode = _setting.GetVnPayTMNCode(); // Terminal Id
            var vnp_RequestId = DateTime.Now.Ticks.ToString(); //Mã hệ thống merchant tự sinh ứng với mỗi yêu cầu truy vấn giao dịch. Mã này là duy nhất dùng để phân biệt các yêu cầu truy vấn giao dịch. Không được trùng lặp trong ngày.
            var vnp_Version = VNPayConstant.VNPAY_VERSION; //2.1.0
            var vnp_Command = "querydr";
            var vnp_TxnRef = transactionCode; // Mã giao dịch thanh toán tham chiếu
            var vnp_OrderInfo = "Truy van giao dich:" + transactionCode;
            var vnp_TransactionDate = transactionDate;
            var vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var vnp_IpAddr = VNPayUtil.GetIpAddress(httpContextAccessor);

            var signData = vnp_RequestId + "|" + vnp_Version + "|" + vnp_Command + "|" + vnp_TmnCode + "|" + vnp_TxnRef + "|" + vnp_TransactionDate + "|" + vnp_CreateDate + "|" + vnp_IpAddr + "|" + vnp_OrderInfo;
            var vnp_SecureHash = VNPayUtil.HmacSHA512(vnp_HashSecret, signData);

            var qdrData = new
            {
                vnp_RequestId = vnp_RequestId,
                vnp_Version = vnp_Version,
                vnp_Command = vnp_Command,
                vnp_TmnCode = vnp_TmnCode,
                vnp_TxnRef = vnp_TxnRef,
                vnp_OrderInfo = vnp_OrderInfo,
                vnp_TransactionDate = vnp_TransactionDate,
                vnp_CreateDate = vnp_CreateDate,
                vnp_IpAddr = vnp_IpAddr,
                vnp_SecureHash = vnp_SecureHash
            };

            var jsonData = JsonSerializer.Serialize(qdrData);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(vnp_Api, content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var queryPaymentResponse = JsonSerializer.Deserialize<QueryPaymentResult>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return queryPaymentResponse;
            }
            return null;
        }
    }
}
