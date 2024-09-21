using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.VNPay.Constant
{
    public class VNPayConstant
    {
        public const string VNPAY_VERSION = "2.1.0";
        public const string VNPAY_URL = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public const string VNPAY_API = "https://sandbox.vnpayment.vn/merchant_webapi/api/transaction";
        public const int VNPAY_EXPIRE_IN_MINUTE = 15;
    }
}
