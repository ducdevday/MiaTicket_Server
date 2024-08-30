using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.DataAccess;
using System.Net;
using MiaTicket.BussinessLogic.Model;
using AutoMapper;
using MiaTicket.Data.Entity;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
namespace MiaTicket.BussinessLogic.Business
{

    public interface IEventBusiness
    {
        Task<CreateEventResponse> CreateEvent(CreateEventRequest request);
        Task<UpdateEventResponse> UpdateEvent(int id, UpdateEventRequest request);

        Task<GetEventResponse> GetEventById(int id);
    }

    public class EventBusiness : IEventBusiness
    {
        private readonly IDataAccessFacade _context;
        private readonly ICloudinaryBusiness _cloudinaryBusiness;
        private readonly IMapper _mapper;

        public EventBusiness(IDataAccessFacade context, ICloudinaryBusiness cloudinaryBusiness, IMapper mapper)
        {
            _context = context;
            _cloudinaryBusiness = cloudinaryBusiness;
            _mapper = mapper;
        }

        public async Task<CreateEventResponse> CreateEvent(CreateEventRequest request)
        {

            //var validation = new CreateEventValidation(request);
            //validation.Validate();
            //if (!validation.IsValid) return new CreateEventResponse(HttpStatusCode.BadRequest, validation.Message, false);

            var userId = await _context.UserData.GetAccountById(request.UserId);
            if (userId == null) return new CreateEventResponse(HttpStatusCode.Conflict, "Account Not Found", false);

            var isCategoryExist = await _context.CategoryData.IsExistCategory(request.CategoryId);
            if (!isCategoryExist) return new CreateEventResponse(HttpStatusCode.Conflict, "Category Not Found", false);


            var logoUrl = _cloudinaryBusiness.UploadFileAsync(request.LogoFile, FileType.EVENT_LOGO_IMAGE);
            var backgroundUrl = _cloudinaryBusiness.UploadFileAsync(request.BackgroundFile, FileType.EVENT_BACKGROUND_IMAGE);
            var organizerLogoUrl = _cloudinaryBusiness.UploadFileAsync(request.OrganizerLogoFile, FileType.ORGANIZER_LOGO_IMAGE);

            await Task.WhenAll([logoUrl, backgroundUrl, organizerLogoUrl]);
            if (logoUrl.Result == null || backgroundUrl.Result == null || organizerLogoUrl.Result == null)
            {
                return new CreateEventResponse(HttpStatusCode.Conflict, "Image Cannot Uploaded", false);
            }
            var evt = _mapper.Map<Event>(request);
            evt.LogoUrl = logoUrl.Result;
            evt.BackgroundUrl = backgroundUrl.Result;
            evt.OrganizerLogoUrl = organizerLogoUrl.Result;

            await _context.EventData.CreateEvent(evt);
            await _context.Commit();
            return new CreateEventResponse(HttpStatusCode.OK, "Create Event Successfully", true);
        }

        public async Task<UpdateEventResponse> UpdateEvent(int id, UpdateEventRequest request)
        {
            //var validation = new UpdateEventValidation(request);
            //validation.Validate();
            //if(!validation.IsValid) return new UpdateEventResponse(HttpStatusCode.BadRequest, validation.Message, false);
            var userId = await _context.UserData.GetAccountById(request.UserId);
            if (userId == null) return new UpdateEventResponse(HttpStatusCode.Conflict, "Account Not Found", false);

            var isCategoryExist = await _context.CategoryData.IsExistCategory(request.CategoryId);
            if (!isCategoryExist) return new UpdateEventResponse(HttpStatusCode.Conflict, "Category Not Found", false);


            var evt = await _context.EventData.GetEventById(id);
            if (evt == null) return new UpdateEventResponse(HttpStatusCode.Conflict, "Event Not Found", false);

            string? logoUrl = null;
            string? backgroundUrl = null;
            string? organizerLogoUrl = null;

            if (request.LogoFile != null) logoUrl = await _cloudinaryBusiness.UploadFileAsync(request.LogoFile, FileType.EVENT_LOGO_IMAGE);
            if (request.BackgroundFile != null) backgroundUrl = await _cloudinaryBusiness.UploadFileAsync(request.BackgroundFile, FileType.EVENT_BACKGROUND_IMAGE);
            if (request.OrganizerLogoFile != null) organizerLogoUrl = await _cloudinaryBusiness.UploadFileAsync(request.OrganizerLogoFile, FileType.ORGANIZER_LOGO_IMAGE);

            _mapper.Map<UpdateEventRequest, Event>(request, evt);
            evt.Id = id;
            if (logoUrl != null) evt.LogoUrl = logoUrl;
            if (backgroundUrl != null) evt.BackgroundUrl = backgroundUrl;
            if (organizerLogoUrl != null) evt.OrganizerLogoUrl = organizerLogoUrl;

            await _context.EventData.UpdateEvent(evt);
            await _context.Commit();
            return new UpdateEventResponse(HttpStatusCode.OK, "Update Event Successfully", true);
        }

        public async Task<GetEventResponse> GetEventById(int id)
        {
            var evt = await _context.EventData.GetEventById(id);
            if (evt == null)
            {
                return new GetEventResponse(HttpStatusCode.BadRequest, "Event Not Found", null);
            }
            var evtDataResponse = _mapper.Map<Event, GetEventDataResponse>(evt);
            await _context.Commit();
            return new GetEventResponse(HttpStatusCode.OK, "Get Event Successfully", evtDataResponse);
        }


    }
}
