using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetEventsByCategoryResponse : BaseApiResponse<List<ByCateEventDto>>
    {
        public GetEventsByCategoryResponse(HttpStatusCode statusCode, string message, List<ByCateEventDto> data) : base(statusCode, message, data)
        {
        }
    }
}
