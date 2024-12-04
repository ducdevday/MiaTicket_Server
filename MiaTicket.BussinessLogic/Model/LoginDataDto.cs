using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class LoginDataDto
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
        public Role Role { get; set; }
    }
}
