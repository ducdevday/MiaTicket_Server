using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class ActivateAccountRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
