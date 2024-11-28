using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class GetEventParticipationTimelineRequest
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
