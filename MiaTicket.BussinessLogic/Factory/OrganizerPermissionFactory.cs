using MiaTicket.BussinessLogic.Stragegy;
using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Factory
{
    public class OrganizerPermissionFactory
    {
        public static IOrganizerPermissionStragegy GetOrganizerPermissionStragegy(OrganizerPermissionType type)
        {
            switch (type)
            {
                case OrganizerPermissionType.OrderReport:
                    return new GetOrderReportPermissionStragegy();
                case OrganizerPermissionType.OrderSummary:
                    return new GetOrderSummaryPermissionStragegy();
                case OrganizerPermissionType.AddMember:
                    return new AddMemberPermissionStragegy();
                case OrganizerPermissionType.EditMember:
                    return new EditMemberPermissionStragegy();
                case OrganizerPermissionType.DeleteMember:
                    return new DeleteMemberPermissionStragegy();
                case OrganizerPermissionType.CheckIn:
                    return new CheckInPermissionStragegy();
                default: 
                    throw new NotImplementedException();
            }            
        }
    }
}
