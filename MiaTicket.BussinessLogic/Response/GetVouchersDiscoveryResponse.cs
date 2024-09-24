using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetVouchersDiscoveryResponse : BaseApiResponse<List<VoucherDiscoveryDto>>
    {
        public GetVouchersDiscoveryResponse(HttpStatusCode statusCode, string message, List<VoucherDiscoveryDto> data) : base(statusCode, message, data)
        {
        }
    }
}
