using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class ShowTime
    {
        public int Id { get; set; }
        public DateTime ShowStartAt { get; set; }
        public DateTime ShowEndAt { get; set; }
        public DateTime SaleStartAt { get; set; }
        public DateTime SaleEndAt { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<Order>? Orders { get; set; }

    }
}
