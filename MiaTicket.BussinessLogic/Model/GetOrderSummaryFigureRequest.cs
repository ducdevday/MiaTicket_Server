using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class GetOrderSummaryFigureRequest
    {
        public int ShowTimeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndTime { get; set; }
    }
}
