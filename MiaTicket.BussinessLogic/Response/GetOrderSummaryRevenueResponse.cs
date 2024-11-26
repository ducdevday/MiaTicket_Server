using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetOrderSummaryRevenueResponse : BaseApiResponse<GetOrderSummaryRevenueDto?>
    {
        public GetOrderSummaryRevenueResponse(HttpStatusCode statusCode, string message, GetOrderSummaryRevenueDto? data) : base(statusCode, message, data)
        {
        }
    }
}
