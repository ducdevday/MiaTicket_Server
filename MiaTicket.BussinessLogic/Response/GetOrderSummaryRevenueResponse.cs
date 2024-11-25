using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetOrderSummaryRevenueResponse : BaseApiResponse<GetOrderSummaryRevenueResponse?>
    {
        public GetOrderSummaryRevenueResponse(HttpStatusCode statusCode, string message, GetOrderSummaryRevenueResponse? data) : base(statusCode, message, data)
        {
        }
    }
}
