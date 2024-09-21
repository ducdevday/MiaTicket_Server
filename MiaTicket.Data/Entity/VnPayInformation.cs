using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class VnPayInformation 
    {
        public int Id { get; set; }
        public string TransactionCode { get; set; }
        public string PaymentUrl { get; set; }
        public double  TotalAmount  { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
