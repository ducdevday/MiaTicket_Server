using MiaTicket.BussinessLogic.Model;
using MiaTicket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetCategoriesDiscoveryResponse : BaseApiResponse<List<CategoryDiscoveryDto>?>
    {
        public GetCategoriesDiscoveryResponse(HttpStatusCode statusCode, string message, List<CategoryDiscoveryDto>? data) : base(statusCode, message, data)
        {

        }
    }
}
