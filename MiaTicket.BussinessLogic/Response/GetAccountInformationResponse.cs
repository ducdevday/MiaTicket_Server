using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetAccountInformationResponse : BaseApiResponse<UserDto?>
    {
        public GetAccountInformationResponse(HttpStatusCode statusCode, string message, UserDto? data) : base(statusCode, message, data)
        {
        }
    }
}
