using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.ZaloPay.Config
{
    public class ZaloPayConfig
    {
            public static string ConfigName => "ZaloPay";
            public string AppUser { get; set; } = string.Empty;
            public string PaymentUrl { get; set; } = string.Empty;
            public string QueryUrl { get; set; } = string.Empty;
            public string RedirectUrl { get; set; } = string.Empty;
            public string CallbackUrl { get; set; } = string.Empty;
            public string IpnUrl { get; set; } = string.Empty;
            public int AppId { get; set; }
            public int ExpireInMinute { get; set; }
    }
}
