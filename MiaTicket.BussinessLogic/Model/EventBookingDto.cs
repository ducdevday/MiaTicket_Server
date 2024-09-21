using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class EventBookingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AddressName { get; set; }
        public string AddressDetail { get; set; }
        public string BackgroundUrl { get; set; }
        public ShowTimeDetailDto ShowTime { get; set; }
    }
}
