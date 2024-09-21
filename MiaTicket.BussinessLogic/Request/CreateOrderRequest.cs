using MiaTicket.BussinessLogic.Model;
using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class CreateOrderRequest
    {
        public int EventId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public int ShowTimeId { get; set; }
        public string? VoucherCode { get; set; }
        public PaymentType PaymentType { get; set; }
        public List<OrderTicketDto> OrderTickets { get; set; }
    }
}
