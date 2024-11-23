using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetOrderReportResponse : BaseApiResponse<List<OrderReportDto>>
    {
        public int TotalRecords { get; set; }
        public GetOrderReportResponse(HttpStatusCode statusCode, string message, List<OrderReportDto> data, int totalRecords = 0) : base(statusCode, message, data)
        {
            TotalRecords = totalRecords;
        }
    }
}
