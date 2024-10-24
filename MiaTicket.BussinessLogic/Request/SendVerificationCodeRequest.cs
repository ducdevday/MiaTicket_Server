using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class SendVerificationCodeRequest
    {
        public string Email { get; set; }

        public int Type { get; set; }
    }
}
