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
            CreateMap<CreateEventRequest, Event>();
            //.ForMember(dest => dest.IsOffline, opt => opt.MapFrom(x => string.IsNullOrEmpty(x.AddressProvince) ? true : false ));
            CreateMap<UpdateEventRequest, Event>();
            CreateMap<ShowTimeDto, ShowTime>();
            CreateMap<TicketDto, Ticket>();

            CreateMap<Event, GetEventDataResponse>();
            CreateMap<ShowTime, ShowTimeDto>();
            CreateMap<Ticket, TicketDto>();

            CreateMap<Category, CategoryDto>();
        }
    }
}
