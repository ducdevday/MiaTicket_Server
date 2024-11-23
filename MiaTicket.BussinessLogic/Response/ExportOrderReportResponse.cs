using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class ExportOrderReportResponse : BaseApiResponse<byte[]?>
    {
        public ExportOrderReportResponse(HttpStatusCode statusCode, string message, byte[]? data) : base(statusCode, message, data)
        {
        }
    }
}
