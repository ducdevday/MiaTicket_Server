using MiaTicket.Setting;
using MiaTicket.ZaloPay.Config;
using MiaTicket.ZaloPay.Extension;
using MiaTicket.ZaloPay.Model;
using MiaTicket.ZaloPay.Util;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace MiaTicket.ZaloPay
{
    public interface IZaloPayService
    {
        Task<CreateZaloPayPaymentResult?> CreatePayment(double totalPrice);
        Task<QueryZaloPayPaymentResult?> QueryPaymentAsync(string transactionCode);
    }

    public class ZaloPayService : IZaloPayService
    {
        private readonly HttpClient _httpClient;
        private readonly ZaloPayConfig _zaloPayConfig;
        private readonly EnviromentSetting _setting = EnviromentSetting.GetInstance();

        public ZaloPayService(HttpClient httpClient, IOptions<ZaloPayConfig> zaloPayConfig)
        {
            _httpClient = httpClient;
            _zaloPayConfig = zaloPayConfig.Value;
        }

        public async Task<CreateZaloPayPaymentResult?> CreatePayment(double totalPrice)
        {
            var content = new FormUrlEncodedContent(GetCreateContent(totalPrice, out string appTransId));
            var response =await _httpClient.PostAsync(_zaloPayConfig.PaymentUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var responseData = JsonSerializer.Deserialize<CreateZaloPayResponse>(responseContent);
                if (responseData != null && responseData.ReturnCode == 1)
                {
                    CreateZaloPayPaymentResult result = new CreateZaloPayPaymentResult()
                    {
                        TransactionCode = appTransId,
                        CreatedAt = DateTime.UtcNow,
                        ExpireAt = DateTime.UtcNow.AddMinutes(_zaloPayConfig.ExpireInMinute),
                        TotalAmount = totalPrice,
                        PaymentUrl = responseData.OrderUrl,
                    };
                    return result;
                }
            }
            return null;
        }

        public async Task<QueryZaloPayPaymentResult?> QueryPaymentAsync(string transactionCode)
        {

            var content = new FormUrlEncodedContent(GetQueryContent(transactionCode));
            var response = await _httpClient.PostAsync(_zaloPayConfig.QueryUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var responseData = JsonSerializer.Deserialize<QueryZaloPayPaymentResult>(responseContent);
                return responseData;

            }
            return null;
        }

        private Dictionary<string, string> GetCreateContent(double totalPrice, out string appTransId)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            var outputIdParam = ZaloPayUtil.GetRandomNumber(8);
            string appId = _zaloPayConfig.AppId.ToString();
            appTransId = DateTime.Now.ToString("yyMMdd") + "_" + outputIdParam;
            string appUser = _zaloPayConfig.AppUser.ToString();
            string amount = totalPrice.ToString();
            string appTime = DateTime.Now.GetTimeStamp().ToString();
            string embed_data = string.Format("{{\"redirecturl\":\"{0}\"}}", _zaloPayConfig.RedirectUrl);
            string item = "[]";
            string key = _setting.GetZaloPayKey1();
            string mac = MakeCreateSignature(appId,appTransId, appUser, amount,appTime,embed_data, item, key);
            keyValuePairs.Add("app_id", appId);
            keyValuePairs.Add("app_user", appUser);
            keyValuePairs.Add("app_time", appTime);
            keyValuePairs.Add("amount", amount);
            keyValuePairs.Add("app_trans_id", appTransId);
            keyValuePairs.Add("description", "MiaTicket Order Payment");
            keyValuePairs.Add("bank_code", "");
            keyValuePairs.Add("item", item);
            keyValuePairs.Add("embed_data", embed_data);
            keyValuePairs.Add("callback_url",_zaloPayConfig.CallbackUrl);
            keyValuePairs.Add("mac", mac);
            return keyValuePairs;
        }

        private string MakeCreateSignature(string appId, string appTransId, string appUser, string amount, string appTime, string embed_data,string item, string key)
        {
            var data = appId + "|" + appTransId + "|" + appUser + "|" + amount + "|"
              + appTime + "|" + embed_data + "|" + item;
            return ZaloPayUtil.HmacSHA256(data, key);
        }

        private Dictionary<string, string> GetQueryContent(string transactionCode)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            string key = _setting.GetZaloPayKey1();
            string mac = MakeQuerySignature(_zaloPayConfig.AppId.ToString(), transactionCode, key);
            keyValuePairs.Add("app_id", _zaloPayConfig.AppId.ToString());
            keyValuePairs.Add("app_trans_id", transactionCode);
            keyValuePairs.Add("mac", mac);
            return keyValuePairs;
        }

        private string MakeQuerySignature(string appId, string appTransId, string key)
        {
            var data = appId + "|" + appTransId + "|" + key;
            return ZaloPayUtil.HmacSHA256(data, key);
        }
    }
}
