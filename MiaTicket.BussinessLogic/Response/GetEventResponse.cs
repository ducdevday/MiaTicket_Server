using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetEventResponse : BaseApiResponse<GetEventDataResponse?>
    {
        public GetEventResponse(HttpStatusCode statusCode, string message, GetEventDataResponse? data) : base(statusCode, message, data)
        {

        }
    }
}
