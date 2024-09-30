using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiaTicket.ZaloPay.Model
{
    public class QueryZaloPayPaymentResult
    {
        [JsonPropertyName("return_code")]
        public int ReturnCode { get; set; }
        [JsonPropertyName("return_message")]
        public string ReturnMessage { get; set; } = string.Empty;
        [JsonPropertyName("is_processing")]
        public bool IsProcessing { get; set; }
        [JsonPropertyName("amount")]
        public long Amount { get; set; }
        [JsonPropertyName("discount_amount")]
        public long DiscountAmount { get; set; }
        [JsonPropertyName("zp_trans_id")]
        public long ZpTransId { get; set; }
    }
}
