using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetWardsResponse : BaseApiResponse<List<WardDto>>
    {
        public GetWardsResponse(HttpStatusCode statusCode, string message, List<WardDto> data) : base(statusCode, message, data)
        {
        }
    }
}
