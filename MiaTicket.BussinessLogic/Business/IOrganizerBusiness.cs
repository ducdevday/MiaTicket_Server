﻿using AutoMapper;
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
            if (validation.IsValid) { 
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

    }

}