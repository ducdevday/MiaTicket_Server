using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class DeleteCategoryResponse : BaseApiResponse<bool>
    {
        public DeleteCategoryResponse(HttpStatusCode statusCode, string message, bool data) : base(statusCode, message, data)
        {
        }
    }
}
