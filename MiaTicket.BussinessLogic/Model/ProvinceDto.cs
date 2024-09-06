using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class ProvinceDto
    {
        [JsonPropertyName("province_id")]
        public string ProvinceId { get; set; }
        [JsonPropertyName("province_name")]
        public string ProvinceName { get; set; }
        [JsonPropertyName("province_type")]
        public string ProvinceType { get; set; }
    }
}
