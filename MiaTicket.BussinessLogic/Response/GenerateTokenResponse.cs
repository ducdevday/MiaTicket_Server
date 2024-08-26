using MiaTicket.BussinessLogic.Model;
using System.Net;

namespace MiaTicket.BussinessLogic.Response
{
    public class GenerateTokenResponse : BaseApiResponse<RefreshTokenDataResponse?>
    {
        public GenerateTokenResponse(HttpStatusCode statusCode, string message, RefreshTokenDataResponse? data) : base(statusCode, message, data)
        {

        }
    }
}
