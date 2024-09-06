using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class WardDto
    {
        [JsonPropertyName("ward_id")]
        public string WardId { get; set; }
        [JsonPropertyName("ward_name")]
        public string WardName { get; set; }
        [JsonPropertyName("ward_type")]
        public string WardType { get; set; }
        [JsonPropertyName("district_id")]
        public string DistrictId { get; set; }
    }
}
