using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class RefreshTokenResponse : BaseApiResponse<RefreshTokenDataResponse?>
    {
        public RefreshTokenResponse(HttpStatusCode statusCode, string message, RefreshTokenDataResponse? data) : base(statusCode, message, data)
        {

        }
    }
}
