using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetLatestEventsResponse : BaseApiResponse<List<LatestEventDto>>
    {
        public GetLatestEventsResponse(HttpStatusCode statusCode, string message, List<LatestEventDto> data) : base(statusCode, message, data)
        {

        }
    }
}
