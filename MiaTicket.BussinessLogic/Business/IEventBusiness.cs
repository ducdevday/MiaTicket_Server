﻿using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.DataAccess;
using System.Net;
using MiaTicket.BussinessLogic.Model;
using AutoMapper;
using MiaTicket.Data.Entity;
using MiaTicket.BussinessLogic.Util;
using MiaTicket.Data.Enum;
using Microsoft.Extensions.Caching.Memory;
using MiaTicket.WebAPI.Constant;
namespace MiaTicket.BussinessLogic.Business
{

    public interface IEventBusiness
    {
        Task<CreateEventResponse> CreateEvent(CreateEventRequest request);
        Task<UpdateEventResponse> UpdateEvent(int id, UpdateEventRequest request);
        Task<DeleteEventResponse> DeleteEvent(int id);
        Task<GetEventResponse> GetEventById(int id);
        Task<GetMyEventsResponse> GetMyEvents(Guid userId, GetMyEventsRequest request);
        Task<GetLatestEventsResponse> GetLatestEvents(GetLatestEventsRequest request);
        Task<GetTrendingEventsResponse> GetTrendingEvents(GetTrendingEventsRequest request);
        Task<GetEventsByCategoryResponse> GetEventsByCategory(GetEventsByCategoryRequest request);
        Task<SearchEventResponse> SearchEvent(SearchEventRequest request);
    }

    public class EventBusiness : IEventBusiness
    {
        private readonly IDataAccessFacade _context;
        private readonly ICloudinaryBusiness _cloudinaryBusiness;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public EventBusiness(IDataAccessFacade context, ICloudinaryBusiness cloudinaryBusiness, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _cloudinaryBusiness = cloudinaryBusiness;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<CreateEventResponse> CreateEvent(CreateEventRequest request)
        {
            var validation = new CreateEventValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new CreateEventResponse(HttpStatusCode.BadRequest, validation.Message, false);

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
            var validation = new UpdateEventValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new UpdateEventResponse(HttpStatusCode.BadRequest, validation.Message, false);
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

        public async Task<DeleteEventResponse> DeleteEvent(int id)
        {
            var evt = await _context.EventData.GetEventById(id);
            if (evt == null)
            {
                return new DeleteEventResponse(HttpStatusCode.BadRequest, "Event Not Found", false);
            }
            await _context.EventData.DeleteEvent(evt);
            await _context.Commit();
            return new DeleteEventResponse(HttpStatusCode.OK, "Delete Event Successfully", true);
        }

        public async Task<GetMyEventsResponse> GetMyEvents(Guid userId, GetMyEventsRequest request)
        {
            var validation = new GetMyEventsValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new GetMyEventsResponse(HttpStatusCode.BadRequest, validation.Message, []);
            var uId = await _context.UserData.GetAccountById(userId);
            if (uId == null) return new GetMyEventsResponse(HttpStatusCode.Conflict, "Account Not Found", []);

            var events = new List<Event>();
            int currentCount = 0;

            // Suppose Do Cachche Event Count Result
            //bool isEventCountExisted = _memoryCache.TryGetValue(AppConstant.EVENT_COUNT_KEYWORD, out currentCount);
            //if (!isEventCountExisted)
            //{
            //    events = await _context.EventData.GetEvents(userId, request.Keyword, request.EventStatus, request.Page, request.Size, out currentCount, false);
            //    _memoryCache.Set(AppConstant.EVENT_COUNT_KEYWORD, currentCount);
            //}
            //else events = await _context.EventData.GetEvents(userId, request.Keyword, request.EventStatus, request.Page, request.Size, out _);
            events = await _context.EventData.GetEvents(userId, request.Keyword, request.EventStatus, request.PageIndex, request.PageSize, out currentCount, false);
            var data = _mapper.Map<List<MyEventDto>>(events);
            return new GetMyEventsResponse(HttpStatusCode.OK, "Get Events Success", data, currentCount);
        }

        public async Task<GetLatestEventsResponse> GetLatestEvents(GetLatestEventsRequest request)
        {
            var validation = new GetLatestEventsValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new GetLatestEventsResponse(HttpStatusCode.BadRequest, validation.Message, []);

            var evts = await _context.EventData.GetLatestEvent(request.Count) ?? [];
            await _context.Commit();
            var dataResponse = _mapper.Map<List<LatestEventDto>>(evts);
            return new GetLatestEventsResponse(HttpStatusCode.OK, "Get Lastest Event Successful", dataResponse);
        }

        public async Task<GetTrendingEventsResponse> GetTrendingEvents(GetTrendingEventsRequest request)
        {
            var validation = new GetTrendingEventsValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new GetTrendingEventsResponse(HttpStatusCode.BadRequest, validation.Message, []);

            var evts = await _context.OrderData.GetTrendingEvent(request.Count) ?? [];
            await _context.Commit();
            var dataResponse = _mapper.Map<List<TrendingEventDto>>(evts);
            return new GetTrendingEventsResponse(HttpStatusCode.OK, "Get Trending Event Successful", dataResponse);
        }

        public async Task<GetEventsByCategoryResponse> GetEventsByCategory(GetEventsByCategoryRequest request)
        {
            var validation = new GetEventsByCategoryValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new GetEventsByCategoryResponse(HttpStatusCode.BadRequest, validation.Message, []);

            bool isCateExist = await _context.CategoryData.IsExistCategory(request.CategoryId);
            if (!isCateExist) return new GetEventsByCategoryResponse(HttpStatusCode.Conflict, "Category Not Exist", []);

            var evts = await _context.EventData.GetEventsByCategory(request.CategoryId, request.Count);
            var dataResponse = _mapper.Map<List<ByCateEventDto>>(evts);
            return new GetEventsByCategoryResponse(HttpStatusCode.OK, "Get Events By Category Successfull", dataResponse);
        }

        public async Task<SearchEventResponse> SearchEvent(SearchEventRequest request)
        {
            var validation = new SearchEventValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new SearchEventResponse(HttpStatusCode.BadRequest, validation.Message, []);

            bool isCateExist = await _context.CategoryData.IsExistCategory(FormaterUtil.SearchEventCategories(request.Categories));
            if (!isCateExist) return new SearchEventResponse(HttpStatusCode.Conflict, "Category Not Exist", []);

            bool isSortTypeValid = await _context.EventData.IsEventSortTypeValid(request.SortBy);
            if (!isSortTypeValid) return new SearchEventResponse(HttpStatusCode.Conflict, "Sort Type Not Exist", []);

            var result = await _context.EventData.SearchEvent(request.Keyword, request.PageIndex, request.PageSize, request.Location, FormaterUtil.SearchEventCategories(request.Categories), FormaterUtil.SearchEventPriceRanges(request.PriceRanges), (EventSortType)request.SortBy);

            var dataResponse = _mapper.Map<List<SearchEventDto>>(result);
            return new SearchEventResponse(HttpStatusCode.OK, "Search Event Success", dataResponse);
        }
    }
}