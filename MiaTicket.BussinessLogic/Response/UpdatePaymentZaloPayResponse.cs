using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class UpdatePaymentZaloPayResponse : BaseApiResponse<ZaloPayInformationDto?>
    {
        public UpdatePaymentZaloPayResponse(HttpStatusCode statusCode, string message, ZaloPayInformationDto? data) : base(statusCode, message, data)
        {
        }
    }
}
