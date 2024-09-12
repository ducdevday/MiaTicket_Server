using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetMyEventsResponse : BaseApiResponse<List<MyEventDto>>
    {
        public int TotalRecords { get; set; }
        public GetMyEventsResponse(HttpStatusCode statusCode, string message, List<MyEventDto> data, int totalRecords = 0) : base(statusCode, message, data)
        {
            TotalRecords = totalRecords;
        }
    }
}
