using MiaTicket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class EventDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AddressName { get; set; }
        public string AddressDetail { get; set; }
        public string BackgroundUrl { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerInformation { get; set; }
        public string OrganizerLogoUrl { get; set; }
        public List<ShowTimeDetailDto> ShowTimes { get; set; }

    }
}
