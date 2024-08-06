using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Event Event { get; set; }
    }
}
