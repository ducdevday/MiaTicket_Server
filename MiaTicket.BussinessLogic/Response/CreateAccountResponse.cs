using System.Net;

namespace MiaTicket.BussinessLogic.Response
{
    public class CreateAccountResponse : BaseApiResponse<string>
    {
        public CreateAccountResponse(HttpStatusCode statusCode, string message, string data) : base(statusCode, message, data)
        {

        }
    }
}
