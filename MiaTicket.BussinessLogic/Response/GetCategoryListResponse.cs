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
    public class GetCategoryListResponse : BaseApiResponse<List<CategoryDto>?>
    {
        public GetCategoryListResponse(HttpStatusCode statusCode, string message, List<CategoryDto>? data) : base(statusCode, message, data)
        {

        }
    }
}
