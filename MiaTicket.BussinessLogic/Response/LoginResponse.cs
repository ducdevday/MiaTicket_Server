using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class LoginResponse : BaseApiResponse<LoginDataResponse?>
    {
        public LoginResponse(HttpStatusCode statusCode, string message, LoginDataResponse? data) : base(statusCode, message, data)
        {
        }
    }
}
