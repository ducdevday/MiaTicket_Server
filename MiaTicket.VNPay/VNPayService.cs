using MiaTicket.Setting;
using MiaTicket.VNPay.Config;
using MiaTicket.VNPay.Library;
using MiaTicket.VNPay.Model;
using MiaTicket.VNPay.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MiaTicket.VNPay
{
    public interface IVNPayService {
        CreateVnPayPaymentResult? CreatePayment(double totalPrice);
        Task<QueryVnPayPaymentResult?> QueryPaymentAsync(string transactionCode, string transactionDate);
    }
    public class VNPayService : IVNPayService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly VNPayConfig _vnpayConfig;
        private readonly EnviromentSetting _setting = EnviromentSetting.GetInstance();
        public VNPayService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient, IOptions<VNPayConfig> vnpayConfig)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _vnpayConfig = vnpayConfig.Value;
        }

        public CreateVnPayPaymentResult? CreatePayment(double totalPrice)
        {
            var vnPayLibrary = new VNPayLibrary();
            var createdAt = DateTime.Now;
            var expireAt = createdAt.AddMinutes(AppConstant.PAYMENT_LINK_EXPIRE_IN_MINUTES);

            var createdUTCAt = DateTime.UtcNow;
            var expireUTCAt = createdUTCAt.AddMinutes(AppConstant.PAYMENT_LINK_EXPIRE_IN_MINUTES);

            string vnp_Returnurl = _vnpayConfig.ReturnUrl; //URL nhan ket qua tra ve 
            string vnp_Url = _vnpayConfig.PaymentUrl; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = _setting.GetVnPayTmnCode(); //Ma website
            string vnp_HashSecret = _setting.GetVnPayHashSecret(); //Chuoi bi mat
            string vnp_Version = _vnpayConfig.Version;
            var vnp_CreateDate = createdAt.ToString("yyyyMMddHHmmss");
            string vnp_IpAddr = VNPayUtil.GetIpAddress(_httpContextAccessor);
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

            CreateVnPayPaymentResult result = new CreateVnPayPaymentResult()
            {
                TransactionCode = vnp_TxnRef,
                CreatedAt = createdUTCAt,
                ExpireAt = expireUTCAt,
                TotalAmount = totalPrice,
                PaymentUrl = paymentUrl,
            };
            return result;
        }

        public async Task<QueryVnPayPaymentResult?> QueryPaymentAsync(string transactionCode, string transactionDate)
        {
            var vnp_Api = _vnpayConfig.PaymentApi;
            var vnp_HashSecret = _setting.GetVnPayHashSecret(); //Secret KEy
            var vnp_TmnCode = _setting.GetVnPayTmnCode(); // Terminal Id
            var vnp_RequestId = DateTime.Now.Ticks.ToString(); //Mã hệ thống merchant tự sinh ứng với mỗi yêu cầu truy vấn giao dịch. Mã này là duy nhất dùng để phân biệt các yêu cầu truy vấn giao dịch. Không được trùng lặp trong ngày.
            var vnp_Version = _vnpayConfig.Version; //2.1.0
            var vnp_Command = "querydr";
            var vnp_TxnRef = transactionCode; // Mã giao dịch thanh toán tham chiếu
            var vnp_OrderInfo = "Truy van giao dich:" + transactionCode;
            var vnp_TransactionDate = transactionDate;
            var vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var vnp_IpAddr = VNPayUtil.GetIpAddress(_httpContextAccessor);

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
                var queryPaymentResponse = JsonSerializer.Deserialize<QueryVnPayPaymentResult>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return queryPaymentResponse;
            }
            return null;
        }
    }
}
