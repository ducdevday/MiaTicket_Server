using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string EventName { get; set; }
        public DateTime ShowTimeStart { get; set; }
        public DateTime ShowTimeEnd { get; set; }
        public string AddressName { get; set; }
        public string AddressDetail { get; set; }
        public List<OrderTicketDetailDto> OrderTickets { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
        public bool IsCanCancel { get; set; } = false;
        public bool IsCanRepayment { get; set; } = false;
        public string PaymentUrl { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public PaymentType PaymentType { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? QrCode { get; set; }
        public string? ReceiverName { get; set; }
        public string? ReceiverEmail { get; set; }
        public string? ReceiverPhoneNumber { get; set; }
    }
}
