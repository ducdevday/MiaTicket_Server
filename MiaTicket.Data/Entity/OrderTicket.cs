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
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }

    }
}
