using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetEventMembersResponse : BaseApiResponse<GetEventMembersDto?>
    {
        public int TotalRecords { get; set; }
        public GetEventMembersResponse(HttpStatusCode statusCode, string message, GetEventMembersDto? data, int totalRecords = 0) : base(statusCode, message, data)
        {
            TotalRecords = totalRecords;
        }
    }
}
