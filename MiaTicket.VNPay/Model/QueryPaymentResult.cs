using System.Text.Json.Serialization;

namespace MiaTicket.VNPay.Response
{

    public class QueryPaymentResult
    {
        [JsonPropertyName("vnp_ResponseId")]
        public string VnpResponseId { get; set; } = string.Empty;

        [JsonPropertyName("vnp_Command")]
        public string VnpCommand { get; set; } = string.Empty;

        [JsonPropertyName("vnp_ResponseCode")]
        public string VnpResponseCode { get; set; } = string.Empty;

        [JsonPropertyName("vnp_Message")]
        public string VnpMessage { get; set; } = string.Empty;

        [JsonPropertyName("vnp_TmnCode")]
        public string VnpTmnCode { get; set; } = string.Empty;

        [JsonPropertyName("vnp_TxnRef")]
        public string VnpTxnRef { get; set; } = string.Empty;

        [JsonPropertyName("vnp_Amount")]
        public string VnpAmount { get; set; } = string.Empty;

        [JsonPropertyName("vnp_OrderInfo")]
        public string VnpOrderInfo { get; set; } = string.Empty;

        [JsonPropertyName("vnp_BankCode")]
        public string VnpBankCode { get; set; } = string.Empty;

        [JsonPropertyName("vnp_PayDate")]
        public string VnpPayDate { get; set; } = string.Empty;

        [JsonPropertyName("vnp_TransactionNo")]
        public string VnpTransactionNo { get; set; } = string.Empty;

        [JsonPropertyName("vnp_TransactionType")]
        public string VnpTransactionType { get; set; } = string.Empty;

        [JsonPropertyName("vnp_TransactionStatus")]
        public string VnpTransactionStatus { get; set; } = string.Empty;

        [JsonPropertyName("vnp_SecureHash")]
        public string VnpSecureHash { get; set; } = string.Empty;
    }

}
