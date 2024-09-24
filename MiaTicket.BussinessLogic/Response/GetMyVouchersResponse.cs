using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetMyVouchersResponse : BaseApiResponse<List<VoucherDto>>
    {
        public string EventName { get; set; }

        public GetMyVouchersResponse(HttpStatusCode statusCode, string message, List<VoucherDto> data, string eventName) : base(statusCode, message, data)
        {
            EventName = eventName;
        }
    }
}
