using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class AddEventMemberRequest
    {
        public string MemberEmail { get; set; }
        public OrganizerPosition MemberRole { get; set; }
    }
}
