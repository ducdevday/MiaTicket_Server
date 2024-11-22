using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetCheckInEventReportResponse : BaseApiResponse<GetCheckInEventReportDto?>
    {
        public GetCheckInEventReportResponse(HttpStatusCode statusCode, string message, GetCheckInEventReportDto? data) : base(statusCode, message, data)
        {
        }
    }
}
