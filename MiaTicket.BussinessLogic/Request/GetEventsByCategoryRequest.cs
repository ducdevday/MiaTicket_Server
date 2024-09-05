using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class GetEventsByCategoryRequest
    {
        public int CategoryId { get; set; }

        public int Count { get; set; }
    }
}
