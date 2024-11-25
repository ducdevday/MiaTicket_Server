using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class TicketSummaryRevenueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int TotalSoldTicket { get; set; }
        public int CapacityTicket { get; set; }
        public double TicketSoldPercentage { get; set; }
    }
}
