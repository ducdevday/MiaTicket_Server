using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class AdminLoginRequest
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
