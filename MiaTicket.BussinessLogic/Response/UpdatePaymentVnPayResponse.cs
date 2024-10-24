using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class UpdatePaymentVnPayResponse : BaseApiResponse<PaymentDto?>
    {
        public UpdatePaymentVnPayResponse(HttpStatusCode statusCode, string message, PaymentDto? data) : base(statusCode, message, data)
        {
        }
    }
}
