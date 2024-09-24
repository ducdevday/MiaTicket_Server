using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class SearchVoucherRequest
    {
        public int EventId { get; set; }
        public string Code { get; set; }
        public int TotalTicketQuantityOfOrder { get; set; }
    }
}
