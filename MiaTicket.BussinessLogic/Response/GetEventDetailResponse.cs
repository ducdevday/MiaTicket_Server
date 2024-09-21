using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetEventDetailResponse : BaseApiResponse<EventDetailDto?>
    {
        public GetEventDetailResponse(HttpStatusCode statusCode, string message, EventDetailDto? data) : base(statusCode, message, data)
        {
        }
    }
}
