using System.Net;

namespace MiaTicket.BussinessLogic.Response
{
    public class ActivateAccountResponse : BaseApiResponse<bool>
    {
        public ActivateAccountResponse(HttpStatusCode statusCode, string message, bool data) : base(statusCode, message, data)
        {
        }
    }
}
