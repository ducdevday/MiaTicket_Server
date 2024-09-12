using MiaTicket.BussinessLogic.Response;
using System.Net;

namespace MiaTicket.BussinessLogic.Model
{
    public class GetMyEventsDataResponse : BaseApiResponse<MyEventDto>
    {
        public GetMyEventsDataResponse(HttpStatusCode statusCode, string message, MyEventDto data) : base(statusCode, message, data)
        {
        }
    }
}
