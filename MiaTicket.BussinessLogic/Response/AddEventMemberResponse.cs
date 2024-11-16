using System.Net;

namespace MiaTicket.BussinessLogic.Response
{
    public class AddEventMemberResponse : BaseApiResponse<bool>
    {
        public AddEventMemberResponse(HttpStatusCode statusCode, string message, bool data) : base(statusCode, message, data)
        {
        }
    }
}
