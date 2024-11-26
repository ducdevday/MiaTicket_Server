using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class GetOrderSummaryRevenueDto
    {
        public double TicketSoldPercentage { get; set; }
        public int TotalSoldTickets { get; set; }
        public int TotalCapacityTickets { get; set; }
        public double GrossSalePercentage { get; set; }
        public double TotalCurrentGrossSale { get; set; }
        public double TotalCapacityGrossSale { get; set; }
        public List<TicketSummaryRevenueDto> Tickets { get; set; }
    }
}
