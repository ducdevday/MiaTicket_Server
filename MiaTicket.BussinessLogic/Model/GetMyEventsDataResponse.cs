using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class GetMyEventsDataResponse : BasePagedResponse<MyEventDto>
    {
        public GetMyEventsDataResponse(int currentPage, int currentSize) : base(currentPage, currentSize)
        {
        }
    }
}
