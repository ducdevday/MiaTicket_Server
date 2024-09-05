using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetBannersDiscoveryResponse : BaseApiResponse<List<BannerDiscoveryDto>>
    {
        public GetBannersDiscoveryResponse(HttpStatusCode statusCode, string message, List<BannerDiscoveryDto> data) : base(statusCode, message, data)
        {
        }
    }
}
