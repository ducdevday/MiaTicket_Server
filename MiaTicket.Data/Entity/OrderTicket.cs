using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class OrderTicket
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public string OrderId { get; set; }
        public OrderTicketStatus OrderTicketStatus { get; set; }
        public string OrderTicketStatisId { get; set; }

    }
}
