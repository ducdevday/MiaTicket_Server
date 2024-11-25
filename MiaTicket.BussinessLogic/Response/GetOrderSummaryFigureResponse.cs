using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetOrderSummaryFigureResponse : BaseApiResponse<List<OrderSummaryFigureDto>>
    {
        public GetOrderSummaryFigureResponse(HttpStatusCode statusCode, string message, List<OrderSummaryFigureDto> data) : base(statusCode, message, data)
        {
        }
    }
}
