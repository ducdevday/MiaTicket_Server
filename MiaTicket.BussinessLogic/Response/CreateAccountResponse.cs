using System.Net;

namespace MiaTicket.BussinessLogic.Response
{
    public class CreateAccountResponse : BaseApiResponse<bool>
    {
        public CreateAccountResponse(HttpStatusCode statusCode, string message, bool data) : base(statusCode, message, data)
        {

        }
    }
}
