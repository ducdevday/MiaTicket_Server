using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class CreateEventResponse: BaseApiResponse<bool>
    {
        public CreateEventResponse(HttpStatusCode statusCode, string message, bool data) : base(statusCode, message, data)
        {

        }
    }
}
