using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.VNPay.Config
{
    public class VNPayConfig
    {
        public static string ConfigName => "VNPay";
        public string Version { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty;
        public string PaymentUrl { get; set; } = string.Empty;
        public string PaymentApi { get; set; } = string.Empty;
        public int ExpireInMinute { get; set; }
    }
}
