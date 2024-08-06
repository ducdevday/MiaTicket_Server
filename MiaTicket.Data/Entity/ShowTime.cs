using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class ShowTime
    {
        public string Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Event Event { get; set; }
        public string EventId { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
