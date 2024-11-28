using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class GetOrderSummaryFigureRequest
    {
        public int ShowTimeId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
