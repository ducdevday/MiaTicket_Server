using MiaTicket.BussinessLogic.Response;
using MiaTicket.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
namespace MiaTicket.BussinessLogic.Business
{
    public interface ISummaryBusiness
    {
        public Task<GetSummaryEventCategoriesResponse> GetSummaryEventCategories(Guid userId);
        public Task<GetDirectoryOrganizersResponse> GetDirectoryOrganizers(Guid userId);
        public Task<GetEventParticipationTimelineResponse> GetEventParticipationTimeline(Guid userId, GetEventParticipationTimelineRequest request);
    }

    public class SummaryBusiness : ISummaryBusiness
    {
        private readonly IDataAccessFacade _context;
        public SummaryBusiness(IDataAccessFacade context)
        {
            _context = context;
        }

        public async Task<GetDirectoryOrganizersResponse> GetDirectoryOrganizers(Guid userId)
        {
            var eventOrganizers = await _context.EventOrganizerData.GetDirectoryOrganizer(userId);
            if (eventOrganizers == null)
            {
                return new GetDirectoryOrganizersResponse(HttpStatusCode.NotFound, "Not Found", []);
            }

            var directoryOrganizersDto = eventOrganizers.GroupBy(eo => eo.Organizer)
                                                         .Select(g => new DirectoryOrganizerDto()
                                                         {
                                                             Id = g.Key.Id,
                                                             Name = g.Key.Name,
                                                             Email = g.Key.Email,
                                                             PhoneNumber = g.Key.PhoneNumber,
                                                             Avatar = g.Key.AvatarUrl ?? ""
                                                         }).ToList();
            return new GetDirectoryOrganizersResponse(HttpStatusCode.OK, "Get Directory Organizer Success", directoryOrganizersDto);
        }

        public async Task<GetSummaryEventCategoriesResponse> GetSummaryEventCategories(Guid userId)
        {
            var eventOrganizers = await _context.EventOrganizerData.GetEventOrganizerByOrganizerId(userId);
            if (eventOrganizers == null)
            {
                return new GetSummaryEventCategoriesResponse(HttpStatusCode.NotFound, "Not Found", []);
            }

            var eventCategoryFigureDtos = eventOrganizers.GroupBy(eo => eo.Event.Category)
                            .Select(g => new EventCategoryFigureDto()
                            {
                                Name = g.Key.Name,
                                Count = g.Count()
                            }).ToList();
            return new GetSummaryEventCategoriesResponse(HttpStatusCode.OK, "Get Summary Event Categories Success", eventCategoryFigureDtos);
        }


        public async Task<GetEventParticipationTimelineResponse> GetEventParticipationTimeline(Guid userId, GetEventParticipationTimelineRequest request)
        {
            var eventOrganizers = await _context.EventOrganizerData.GetEventParticipationTimeline(userId,FormaterUtil.ConvertISOStringToUTCDate(request.StartDate), FormaterUtil.ConvertISOStringToUTCDate(request.EndDate));
            if (eventOrganizers == null)
            {
                return new GetEventParticipationTimelineResponse(HttpStatusCode.NotFound, "Not Found", []);
            }

            var eventParticipationsDto = eventOrganizers.GroupBy(eo => new
            {
                Year = eo.CreatedAt.Year,
                Month = eo.CreatedAt.Month,
                Day = eo.CreatedAt.Day
            }).Select(g => new EventParticipationDto() {
                Count = g.Count(),
                Time = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day)
            }).ToList();

            return new GetEventParticipationTimelineResponse(HttpStatusCode.OK, "Get Event Participation Timeline Success", eventParticipationsDto);
        }
    }
}
