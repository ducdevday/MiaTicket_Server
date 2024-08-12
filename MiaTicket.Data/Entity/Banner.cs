using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class Banner
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}
