using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class Banner
    {
        public string Id { get; set; }
        public string VideoUrl { get; set; }
        public Event Event { get; set; }
        public string EventId { get; set; }
    }
}
