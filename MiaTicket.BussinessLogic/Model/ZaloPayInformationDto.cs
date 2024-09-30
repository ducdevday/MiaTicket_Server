using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class ZaloPayInformationDto
    {
        public string TransactionCode { get; set; }
        public string PaymentUrl { get; set; }
        public double TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
