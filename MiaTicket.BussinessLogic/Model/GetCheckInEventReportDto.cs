using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class GetCheckInEventReportDto
    {
        public double TicketCheckedInPercentage { get; set; }
        public int TotalCheckedInTickets { get; set; }
        public int TotalPaidTickets { get; set; }
        public List<TicketCheckInDto> Tickets { get; set; }
    }
}
