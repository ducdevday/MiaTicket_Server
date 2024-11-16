using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class GetEventMembersDto
    {
        public string EventName { get; set; }
        public bool CanAddNewMembers { get; set; }
        public List<OrganizerPosition> AddAbleRoles { get; set; }
        public List<MemberDto> Members { get; set; }
    }
}
