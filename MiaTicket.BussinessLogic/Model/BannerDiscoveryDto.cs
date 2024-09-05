using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class BannerDiscoveryDto
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
    }
}
