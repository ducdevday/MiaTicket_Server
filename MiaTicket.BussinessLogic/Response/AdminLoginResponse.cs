using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class AdminLoginResponse : BaseApiResponse<LoginDataDto?>
    {
        public AdminLoginResponse(HttpStatusCode statusCode, string message, LoginDataDto? data) : base(statusCode, message, data)
        {

        }
    }
}
