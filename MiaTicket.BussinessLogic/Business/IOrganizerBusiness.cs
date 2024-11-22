using AutoMapper;
using AutoMapper.Execution;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.DataAccess;
using System.Net;
using MiaTicket.Data.Enum;
using MiaTicket.Data.Entity;
using Azure.Core;

namespace MiaTicket.BussinessLogic.Business
{
    public interface IOrganizerBusiness
    {
        public Task<GetEventMembersResponse> GetEventMembers(Guid userId,int eventId, GetEventMembersRequest request);
        public Task<UpdateEventMemeberResponse> UpdateEventMember(Guid userId, int eventId, Guid memberId, UpdateEventMemberRequest request);
        public Task<DeleteEventMemberResponse> DeleteEventMember(Guid userId, int eventId, Guid memberId);
        public Task<AddEventMemberResponse> AddEventMember(Guid userId, int eventId, AddEventMemberRequest request);
        public Task<CheckInEventResponse> CheckInEvent(Guid userId, int eventId, CheckInEventRequest request);
        public Task<GetCheckInEventReportResponse> GetCheckInEventReport(Guid userId, int eventId, GetCheckInEventReportRequest request);
    }

    public class OrganizerBusiness : IOrganizerBusiness
    {
        private readonly IDataAccessFacade _context;
        private readonly IMapper _mapper;

        public OrganizerBusiness(IDataAccessFacade context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetEventMembersResponse> GetEventMembers(Guid userId, int eventId, GetEventMembersRequest request)
        {
           var validate = new GetEventMembersValidation(request);
           validate.Validate();
           if (!validate.IsValid) return new GetEventMembersResponse(HttpStatusCode.BadRequest, validate.Message, null);

            var getEventNameTask = _context.EventData.GetEventName(eventId);
            var getEventMembersTask = _context.EventOrganizerData.GetEventMembers(eventId, request.Keyword, request.PageIndex, request.PageSize, out int totalRecords);
            var getCurrentEventMemberTask = _context.EventOrganizerData.GetEventOrganizerById(eventId, userId);
            // Await both tasks in parallel
            await Task.WhenAll(getEventNameTask, getEventMembersTask);

            // Retrieve the results from the tasks
            var eventName = getEventNameTask.Result ;
            var members = getEventMembersTask.Result;
            var currentMember = getCurrentEventMemberTask.Result;

            if (eventName == null || members == null || currentMember == null) {
                return new GetEventMembersResponse(HttpStatusCode.NotFound, "Not Fount Event Members", null);
            }

            List<MemberDto> memberDtos = new List<MemberDto>();
            var currentMemberDto = _mapper.Map<MemberDto>(currentMember);
            foreach (var member in members) { 
                var m = _mapper.Map<MemberDto>(member);
                m.IsAbleToEdit = CheckAbleToEdit(currentMemberDto, m);
                m.IsAbleToDelete = CheckAbleToDelete(currentMemberDto, m);
                memberDtos.Add(m);
            }

            var getEventMemberDto = new GetEventMembersDto() {
                EventName = eventName,
                CanAddNewMembers = CheckAbleToAddMember(currentMemberDto),
                AddAbleRoles = CheckListAddAbleRole(currentMemberDto),
                Members = memberDtos
            };
            return new GetEventMembersResponse(HttpStatusCode.OK, "Get Event Members Success", getEventMemberDto, totalRecords);
        }


        public async Task<UpdateEventMemeberResponse> UpdateEventMember(Guid userId, int eventId, Guid memberId, UpdateEventMemberRequest request)
        {
            var getCurrentEventMemberTask = _context.EventOrganizerData.GetEventOrganizerById(eventId, userId);
            var getChoosenEventMemberTask = _context.EventOrganizerData.GetEventOrganizerById(eventId, memberId);
            await Task.WhenAll(getCurrentEventMemberTask, getChoosenEventMemberTask);

            var currentMember = getCurrentEventMemberTask.Result;
            var choosenMember = getChoosenEventMemberTask.Result;

            if (currentMember == null || choosenMember == null)
            {
                return new UpdateEventMemeberResponse(HttpStatusCode.NotFound, "Not Fount Event Member", false);
            }

            MemberDto currentMemberDto = _mapper.Map<MemberDto>(getCurrentEventMemberTask.Result);
            MemberDto choosenMemberDto = _mapper.Map<MemberDto>(getChoosenEventMemberTask.Result);

            if (!CheckAbleToEdit(currentMemberDto, choosenMemberDto)) { 
                return new UpdateEventMemeberResponse(HttpStatusCode.Forbidden, "No Permission", false);
            }


            choosenMember.Position = request.Role;
            var updatedMember = await _context.EventOrganizerData.UpdateEventMember(choosenMember);
            await _context.Commit();

            return new UpdateEventMemeberResponse(HttpStatusCode.OK, "Update Member Success", true);
        }

        public async Task<DeleteEventMemberResponse> DeleteEventMember(Guid userId, int eventId, Guid memberId)
        {

            var getCurrentEventMemberTask = _context.EventOrganizerData.GetEventOrganizerById(eventId, userId);
            var getChoosenEventMemberTask = _context.EventOrganizerData.GetEventOrganizerById(eventId, memberId);
            await Task.WhenAll(getCurrentEventMemberTask, getChoosenEventMemberTask);

            var currentMember = getCurrentEventMemberTask.Result;
            var choosenMember = getChoosenEventMemberTask.Result;

            if (currentMember == null || choosenMember == null)
            {
                return new DeleteEventMemberResponse(HttpStatusCode.NotFound, "Not Fount Event Member", false);
            }

            MemberDto currentMemberDto = _mapper.Map<MemberDto>(getCurrentEventMemberTask.Result);
            MemberDto choosenMemberDto = _mapper.Map<MemberDto>(getChoosenEventMemberTask.Result);

            if (!CheckAbleToDelete(currentMemberDto, choosenMemberDto))
            {
                return new DeleteEventMemberResponse(HttpStatusCode.Forbidden, "No Permission", false);
            }

            await _context.EventOrganizerData.DeleteEventMember(choosenMember);

            await _context.Commit();

            return new DeleteEventMemberResponse(HttpStatusCode.OK, "Delete Member Success", true);
        }

        public async Task<AddEventMemberResponse> AddEventMember(Guid userId, int eventId, AddEventMemberRequest request)
        {
            var validation = new AddEventMemberValidation(request);
            validation.Validate();
            if (!validation.IsValid) { 
                return new AddEventMemberResponse(HttpStatusCode.BadRequest, validation.Message, false);
            }
            var getCurrentEventMemberTask = _context.EventOrganizerData.GetEventOrganizerById(eventId, userId);
            var getEventTask = _context.EventData.GetEventById(eventId);
            var getUserTask = _context.UserData.GetAccountByEmail(request.MemberEmail);
            await Task.WhenAll([getCurrentEventMemberTask, getEventTask, getUserTask]);

            var currentMember = getCurrentEventMemberTask.Result;
            var evt = getEventTask.Result;
            var user = getUserTask.Result;

            if (currentMember == null) {
                return new AddEventMemberResponse(HttpStatusCode.NotFound, "Event Member Found", false);
            }
            if (evt == null) { 
                return new AddEventMemberResponse(HttpStatusCode.NotFound, "Event Not Found", false);
            }
            if (user == null) {
                return new AddEventMemberResponse(HttpStatusCode.NotFound, "User Not Found", false);
            }
            if (user.Role != Role.Organizer) { 
                return new AddEventMemberResponse(HttpStatusCode.Conflict, "Only Organizer Can Be Added", false);
            }

            MemberDto currentMemberDto = _mapper.Map<MemberDto>(currentMember);
            if (!CheckAbleToAddMember(currentMemberDto)) {
                return new AddEventMemberResponse(HttpStatusCode.Forbidden, "No Permission", false);
            }

            var isMemberExist = await _context.EventOrganizerData.IsMemberExist(eventId, request.MemberEmail);
            if (isMemberExist) {
                return new AddEventMemberResponse(HttpStatusCode.Conflict, "Member Is Exist", false);
            }

            var eventOrganizer = new EventOrganizer()
            {
                EventId = evt.Id,
                Event = evt,
                OrganizerId = user.Id,
                Organizer = user,
                Position = request.MemberRole
            };

            await _context.EventOrganizerData.AddEventMember(eventOrganizer);
            await _context.Commit();

            return new AddEventMemberResponse(HttpStatusCode.OK, "Add Event Member Success", true);
        }

        public async Task<CheckInEventResponse> CheckInEvent(Guid userId, int eventId, CheckInEventRequest request)
        {
            var validation = new CheckInEventValidation(request);
            validation.Validate();
            if (!validation.IsValid)
            {
                return new CheckInEventResponse(HttpStatusCode.BadRequest, validation.Message, []);
            }

            var eventOrganizer = await _context.EventOrganizerData.GetEventOrganizerById(eventId, userId);
            if (eventOrganizer == null) {
                return new CheckInEventResponse(HttpStatusCode.NotFound, "Event Or User Invalid", []);
            }
            var organizerDto = _mapper.Map<MemberDto>(eventOrganizer);

            if (!CheckAbleToCheckIn(organizerDto)) { 
                return new CheckInEventResponse(HttpStatusCode.Forbidden, "No Permission", []);
            }

            var eventCheckIn = await _context.EventCheckInData.GetEventCheckIn(request.Code);
            if (eventCheckIn == null)
            {
                return new CheckInEventResponse(HttpStatusCode.NotFound, "Code Is Not Exist", []);
            }
            else if (eventCheckIn.IsCheckedIn) { 
                return new CheckInEventResponse(HttpStatusCode.Conflict, "Code Is Used", []);
            }
            eventCheckIn.IsCheckedIn = true;
            eventCheckIn.CheckedInAt = DateTime.UtcNow;
            eventCheckIn.OrganizerId = eventOrganizer.OrganizerId;
            eventCheckIn.Organizer = eventOrganizer.Organizer;

            var tickets =_mapper.Map<List<OrderTicketDetailDto>>(eventCheckIn.Order.OrderTickets);

            await _context.EventCheckInData.UpdateEventCheckIn(eventCheckIn);
            await _context.Commit();

            return new CheckInEventResponse(HttpStatusCode.OK, "Check In Success", tickets);
        }

        public async Task<GetCheckInEventReportResponse> GetCheckInEventReport(Guid userId, int eventId, GetCheckInEventReportRequest request)
        {
            var eventOrganizer = await _context.EventOrganizerData.GetEventOrganizerById(eventId, userId);
            if (eventOrganizer == null)
            {
                return new GetCheckInEventReportResponse(HttpStatusCode.NotFound, "Event Or User Invalid", null);
            }
            var organizerDto = _mapper.Map<MemberDto>(eventOrganizer);

            if (!CheckAbleToCheckIn(organizerDto))
            {
                return new GetCheckInEventReportResponse(HttpStatusCode.Forbidden, "No Permission", null);
            }

            var orders = await _context.EventData.GetCheckInReportByShowTime(eventId, request.ShowTimeId);

            if (orders == null) {
                return new GetCheckInEventReportResponse(HttpStatusCode.NotFound, "ShowTime Not Exist", null);
            }

            var totalPaidTickets = orders.Where(x => x.Payment.PaymentStatus == PaymentStatus.Paid).SelectMany(x => x.OrderTickets).Sum(x => x.Quantity);
            var totalCheckedInTickets = orders.Where(x => x.Payment.PaymentStatus == PaymentStatus.Paid && x.EventCheckIn.IsCheckedIn == true).SelectMany(x => x.OrderTickets).Sum(x => x.Quantity);
            var checkInPercent = totalPaidTickets > 0 ? (totalCheckedInTickets / totalPaidTickets) * 100 : 0;

            var tickets = orders.Where(x => x.Payment.PaymentStatus == PaymentStatus.Paid).SelectMany(x => x.OrderTickets)
                                         .GroupBy(x => new {x.TicketId, x.Name, x.Price})
                                         .Select(g => new TicketCheckInDto()
                                         {
                                             Id = g.Key.TicketId,
                                             Name = g.Key.Name,
                                             Price = g.Key.Price,
                                             TotalPaidTicket = g.Sum(x => x.Quantity),
                                             TotalCheckedInTicket = g.Where(x =>x.Order.EventCheckIn.IsCheckedIn == true).Sum(x => x.Quantity),
                                             TicketCheckedInPercentage = g.Sum(x => x.Quantity) > 0 ? (g.Where(x => x.Order.EventCheckIn.IsCheckedIn == true).Sum(x => x.Quantity) / g.Sum(x => x.Quantity)) * 100 : 0
                                         }).ToList();

            var dataResponse = new GetCheckInEventReportDto()
            {
                TotalPaidTickets = totalPaidTickets,
                TotalCheckedInTickets = totalCheckedInTickets,
                TicketCheckedInPercentage = checkInPercent,
                Tickets = tickets,
            };

            return new GetCheckInEventReportResponse(HttpStatusCode.OK, "Get Check In Event Report Success", dataResponse);
        }
        private bool CheckAbleToAddMember(MemberDto currentMemberDto) {
            switch (currentMemberDto.Role) {
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

        private List<OrganizerPosition> CheckListAddAbleRole(MemberDto currentMemberDto) {
            switch (currentMemberDto.Role)
            {
                case OrganizerPosition.Owner:
                    return [OrganizerPosition.Moderator, OrganizerPosition.Coordinator];
                case OrganizerPosition.Moderator:
                    return [OrganizerPosition.Coordinator];
                case OrganizerPosition.Coordinator:
                    return [];
                default:
                    return [];
            }
        }
        
        private bool CheckAbleToEdit(MemberDto currentMemberDto, MemberDto member) {
            if(currentMemberDto.MemberId == member.MemberId)
                return false;
            if (currentMemberDto.Role == OrganizerPosition.Owner)
                return true;
            else if (currentMemberDto.Role == OrganizerPosition.Moderator && member.Role == OrganizerPosition.Coordinator)
                return true;
            else if (currentMemberDto.Role == OrganizerPosition.Coordinator)
                return false;

            return false;
        }

        private bool CheckAbleToDelete(MemberDto currentMemberDto, MemberDto member)
        {
            if (currentMemberDto.MemberId == member.MemberId)
                return false;
            if (currentMemberDto.Role == OrganizerPosition.Owner)
                return true;
            else if (currentMemberDto.Role == OrganizerPosition.Moderator && member.Role == OrganizerPosition.Coordinator)
                return true;
            else if (currentMemberDto.Role == OrganizerPosition.Coordinator)
                return false;

            return false;
        }

        private bool CheckAbleToCheckIn(MemberDto currentMemberDto)
        {
            switch (currentMemberDto.Role)
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
