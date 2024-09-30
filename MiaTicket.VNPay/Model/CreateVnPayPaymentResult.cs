using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.VNPay.Model
{
    public class CreateVnPayPaymentResult
    {
        public string TransactionCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public double TotalAmount { get; set; }
        public string PaymentUrl { get; set; }
    }
}
