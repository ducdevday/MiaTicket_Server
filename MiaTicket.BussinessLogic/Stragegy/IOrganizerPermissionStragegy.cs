using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Stragegy
{
    public interface IOrganizerPermissionStragegy
    {
        bool IsHavePermission(OrganizerPosition currentPosition, OrganizerPosition? memberPosition = null, Guid? currentId = null, Guid? memberId = null);

    }

    public class GetOrderReportPermissionStragegy : IOrganizerPermissionStragegy
    {
        public bool IsHavePermission(OrganizerPosition currentPosition, OrganizerPosition? memberPosition = null, Guid? currentId = null, Guid? memberId = null)
        {
            switch (currentPosition)
            {
                case OrganizerPosition.Owner:
                    return true;
                case OrganizerPosition.Moderator:
                    return true;
                case OrganizerPosition.Coordinator:
                    return false;
                default:
                    return false;
            }
        }
    }

    public class GetOrderSummaryPermissionStragegy : IOrganizerPermissionStragegy
    {
        public bool IsHavePermission(OrganizerPosition currentPosition, OrganizerPosition? memberPosition = null, Guid? currentId = null, Guid? memberId = null)
        {
            switch (currentPosition)
            {
                case OrganizerPosition.Owner:
                    return true;
                case OrganizerPosition.Moderator:
                    return false;
                case OrganizerPosition.Coordinator:
                    return false;
                default:
                    return false;
            }
        }
    }

    public class AddMemberPermissionStragegy : IOrganizerPermissionStragegy
    {
        public bool IsHavePermission(OrganizerPosition currentPosition, OrganizerPosition? memberPosition = null, Guid? currentId = null, Guid? memberId = null)
        {
            switch (currentPosition)
            {
                case OrganizerPosition.Owner:
                    return true;
                case OrganizerPosition.Moderator:
                    return true;
                case OrganizerPosition.Coordinator:
                    return false;
                default:
                    return false;
            }
        }
    }

    public class EditMemberPermissionStragegy : IOrganizerPermissionStragegy
    {
        public bool IsHavePermission(OrganizerPosition currentPosition, OrganizerPosition? memberPosition = null, Guid? currentId = null, Guid? memberId = null)
        {
            if (currentId == memberId)
                return false;
            if (currentPosition == OrganizerPosition.Owner)
                return true;
            else if (currentPosition == OrganizerPosition.Moderator && memberPosition == OrganizerPosition.Coordinator)
                return true;
            else if (currentPosition == OrganizerPosition.Coordinator)
                return false;

            return false;
        }
    }

    public class DeleteMemberPermissionStragegy : IOrganizerPermissionStragegy
    {
        public bool IsHavePermission(OrganizerPosition currentPosition, OrganizerPosition? memberPosition = null, Guid? currentId = null, Guid? memberId = null)
        {
            if (currentId == memberId)
                return false;
            if (currentPosition == OrganizerPosition.Owner)
                return true;
            else if (currentPosition == OrganizerPosition.Moderator && memberPosition == OrganizerPosition.Coordinator)
                return true;
            else if (currentPosition == OrganizerPosition.Coordinator)
                return false;

            return false;
        }
    }

    public class CheckInPermissionStragegy : IOrganizerPermissionStragegy
    {
        public bool IsHavePermission(OrganizerPosition currentPosition, OrganizerPosition? memberPosition = null, Guid? currentId = null, Guid? memberId = null)
        {
            switch (currentPosition)
            {
                case OrganizerPosition.Owner:
                    return true;
                case OrganizerPosition.Moderator:
                    return true;
                case OrganizerPosition.Coordinator:
                    return true;
                default:
                    return false;
            }
        }
    }
}
