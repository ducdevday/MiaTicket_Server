using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetEventBookingResponse : BaseApiResponse<EventBookingDto?>
    {
        public GetEventBookingResponse(HttpStatusCode statusCode, string message, EventBookingDto? data) : base(statusCode, message, data)
        {
        }
    }
}
