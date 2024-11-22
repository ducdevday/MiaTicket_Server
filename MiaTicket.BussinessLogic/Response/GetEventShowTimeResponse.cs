using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetEventShowTimeResponse : BaseApiResponse<List<ShowTimeDetailDto>>
    {
        public GetEventShowTimeResponse(HttpStatusCode statusCode, string message, List<ShowTimeDetailDto> data) : base(statusCode, message, data)
        {
        }
    }
}
