using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class OrderSummaryFigureDto
    {
        public double TotalAmount { get; set; }
        public int TotalTicketSold { get; set; }
        public DateTime Time {  get; set; }
    }
}
