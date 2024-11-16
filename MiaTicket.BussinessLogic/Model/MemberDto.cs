using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class MemberDto
    {
        public Guid MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public OrganizerPosition Role { get; set; }
        public bool IsAbleToEdit { get; set; }
        public bool IsAbleToDelete { get; set; }
    }
}
