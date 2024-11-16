using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetEventNameResponse : BaseApiResponse<string>
    {
        public GetEventNameResponse(HttpStatusCode statusCode, string message, string data) : base(statusCode, message, data)
        {

        }
    }
}
