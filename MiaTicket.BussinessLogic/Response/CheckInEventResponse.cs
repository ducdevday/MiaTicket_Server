using MiaTicket.BussinessLogic.Model;
using MiaTicket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class CheckInEventResponse : BaseApiResponse<List<OrderTicketDetailDto>>
    {
        public CheckInEventResponse(HttpStatusCode statusCode, string message, List<OrderTicketDetailDto> data) : base(statusCode, message, data)
        {
        }
    }
}
