using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetTrendingEventsResponse : BaseApiResponse<List<TrendingEventDto>>
    {
        public GetTrendingEventsResponse(HttpStatusCode statusCode, string message, List<TrendingEventDto> data) : base(statusCode, message, data)
        {
        }
    }
}
