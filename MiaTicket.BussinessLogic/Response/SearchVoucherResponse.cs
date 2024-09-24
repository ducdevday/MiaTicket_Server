using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class SearchVoucherResponse : BaseApiResponse<SearchVoucherDto?>
    {
        public SearchVoucherResponse(HttpStatusCode statusCode, string message, SearchVoucherDto? data) : base(statusCode, message, data)
        {
        }
    }
}
