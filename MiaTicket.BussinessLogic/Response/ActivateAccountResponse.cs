using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class ActivateAccountResponse : BaseApiResponse<bool>
    {
        public ActivateAccountResponse(HttpStatusCode statusCode, string message, bool data) : base(statusCode, message, data)
        {
        }
    }
}
