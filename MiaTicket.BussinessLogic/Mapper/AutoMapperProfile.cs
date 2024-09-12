using AutoMapper;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Entity;

namespace MiaTicket.BussinessLogic.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //**********************************************EVENT MAPPER***********************************************************
            //---------------------------------------------------------------------------------------------------------------------

            CreateMap<CreateEventRequest, Event>();
            CreateMap<UpdateEventRequest, Event>();
            CreateMap<ShowTimeDto, ShowTime>();
            CreateMap<TicketDto, Ticket>();

            CreateMap<Event, GetEventDataResponse>();
            CreateMap<ShowTime, ShowTimeDto>();
            CreateMap<Ticket, TicketDto>();

            CreateMap<Event, MyEventDto>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.BackgroundUrl))
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                        .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.ShowTimes.First().ShowStartAt))
                        .ForMember(dest => dest.VenueName, opt => opt.MapFrom(src => src.AddressName));

            CreateMap<Event, LatestEventDto>().ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.LogoUrl));

            CreateMap<Event, HomeEventDto>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug))
                        .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.BackgroundUrl))
                        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ShowTimes.First().Tickets.Min(t => t.Price)))
                        .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.ShowTimes.First().ShowStartAt));

            CreateMap<Event, TrendingEventDto>().IncludeBase<Event, HomeEventDto>();
            CreateMap<Event, ByCateEventDto>().IncludeBase<Event, HomeEventDto>();
            CreateMap<Event, SearchEventDto>().IncludeBase<Event, HomeEventDto>();

            //**********************************************BANNER MAPPER***********************************************************
            //----------------------------------------------------------------------------------------------------------------------

            CreateMap<Banner, BannerDiscoveryDto>()
                            .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Event.Name))
                            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Event.Slug))
                            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Event.BackgroundUrl))
                            .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.VideoUrl))
                            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Event.ShowTimes.First().Tickets.Min(t => t.Price)))
                            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Event.ShowTimes.First().ShowStartAt));

            //**********************************************CATEGORY MAPPER***********************************************************
            //------------------------------------------------------------------------------------------------------------------------
            //GetMyEventsDataResponse
            CreateMap<Category, CategoryDiscoveryDto>();
        }
    }
}
