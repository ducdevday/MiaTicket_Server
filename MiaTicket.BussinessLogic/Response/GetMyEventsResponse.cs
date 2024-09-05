using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetMyEventsResponse : BaseApiResponse<GetMyEventsDataResponse?>
    {
        public GetMyEventsResponse(HttpStatusCode statusCode, string message, GetMyEventsDataResponse? data) : base(statusCode, message, data)
        {
        }
    }
}
