using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetMyOrdersResponse : BaseApiResponse<List<MyOrderDto>>
    {
        public int TotalRecords { get; set; }
        public GetMyOrdersResponse(HttpStatusCode statusCode, string message, List<MyOrderDto> data, int totalRecords = 0) : base(statusCode, message, data)
        {
            TotalRecords = totalRecords;
        }
    }
}
