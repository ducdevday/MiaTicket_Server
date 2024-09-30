using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiaTicket.ZaloPay.Model
{
    internal class CreateZaloPayResponse
    {
        [JsonPropertyName("return_code")]
        public int ReturnCode { get; set; }
        [JsonPropertyName("return_message")]
        public string ReturnMessage { get; set; } = string.Empty;
        [JsonPropertyName("order_url")]
        public string OrderUrl { get; set; } = string.Empty;
    }
}
