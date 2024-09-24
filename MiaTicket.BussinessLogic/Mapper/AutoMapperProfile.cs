using AutoMapper;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Entity;
using MiaTicket.VNPay.Model;
using MiaTicket.Data.Enum;

namespace MiaTicket.BussinessLogic.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //**********************************************ACCOUNT MAPPER***********************************************************
            CreateMap<User, UserDto>();

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

            CreateMap<Event, EventDetailDto>()
                        .ForMember(des => des.AddressDetail, opt => opt.MapFrom(src => $"{src.AddressNo}, {src.AddressWard}, {src.AddressDistrict}, {src.AddressProvince}"));
            CreateMap<ShowTime, ShowTimeDetailDto>();
            CreateMap<Ticket, TicketDetailDto>()
                        .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.Quantity > 0))
                        .ForMember(dest => dest.MinimumPurchase, opt => opt.MapFrom(src => src.MinimumPurchase < src.Quantity ? src.MinimumPurchase : src.Quantity))
                        .ForMember(des => des.MaximumPurchase, opt => opt.MapFrom(src => src.MaximumPurchase < src.Quantity ? src.MaximumPurchase : src.Quantity));

            CreateMap<Event, EventBookingDto>()
                        .ForMember(des => des.AddressDetail, opt => opt.MapFrom(src => $"{src.AddressNo}, {src.AddressWard}, {src.AddressDistrict}, {src.AddressProvince}"))
                        .ForMember(des => des.ShowTime, opt => opt.MapFrom(src => src.ShowTimes[0]));

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


            //**********************************************ORDER MAPPER**************************************************************
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<OrderTicketDto, OrderTicket>().ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.TicketId))
                                                    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<CreatePaymentResult, VnPayInformation>();
            CreateMap<VnPayInformation, VnPayInformationDto>().ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Order.OrderStatus));

            CreateMap<OrderTicket, OrderTicketDetailDto>()
                                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                                        .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                                        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
            CreateMap<Order, MyOrderDto>()
                                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                                        .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.BackgroundUrl))
                                        .ForMember(dest => dest.ShowTimeStart, opt => opt.MapFrom(src => src.DateStart))
                                        .ForMember(dest => dest.ShowTimeEnd, opt => opt.MapFrom(src => src.DateEnd))
                                        .ForMember(dest => dest.AddressName, opt => opt.MapFrom(src => src.AddressName))
                                        .ForMember(dest => dest.AddressDetail, opt => opt.MapFrom(src => src.AddressDetail))
                                        .ForMember(dest => dest.OrderTickets, opt => opt.MapFrom(src => src.OrderTickets))
                                        .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                                        .ForMember(dest => dest.IsCanCancel, opt => opt.MapFrom(src =>src.OrderStatus == OrderStatus.Pending &&(src.VnPayInformation == null || src.VnPayInformation.PaymentStatus == PaymentStatus.UnPaid)))
                                        .ForMember(dest => dest.IsCanRepayment, opt => opt.MapFrom(src => src.OrderStatus == OrderStatus.Pending && (src.VnPayInformation == null || src.VnPayInformation.PaymentStatus == PaymentStatus.UnPaid && DateTime.UtcNow <= src.VnPayInformation.ExpireAt)))
                                        .ForMember(dest => dest.PaymentUrl, opt => opt.MapFrom(src => src.VnPayInformation != null ? src.VnPayInformation.PaymentUrl : string.Empty));

            CreateMap<Order, OrderDetailDto>()
                                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                                        .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.BackgroundUrl))
                                        .ForMember(dest => dest.ShowTimeStart, opt => opt.MapFrom(src => src.DateStart))
                                        .ForMember(dest => dest.ShowTimeEnd, opt => opt.MapFrom(src => src.DateEnd))
                                        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                                        .ForMember(dest => dest.AddressName, opt => opt.MapFrom(src => src.AddressName))
                                        .ForMember(dest => dest.AddressDetail, opt => opt.MapFrom(src => src.AddressDetail))
                                        .ForMember(dest => dest.OrderTickets, opt => opt.MapFrom(src => src.OrderTickets))
                                        .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => (src.OrderTickets.Sum(x => x.Price * x.Quantity) - src.TotalPrice )>0 ? (src.OrderTickets.Sum(x => x.Price * x.Quantity) - src.TotalPrice) : 0))
                                        .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                                        .ForMember(dest => dest.IsCanCancel, opt => opt.MapFrom(src => src.OrderStatus == OrderStatus.Pending && (src.VnPayInformation == null || src.VnPayInformation.PaymentStatus == PaymentStatus.UnPaid)))
                                        .ForMember(dest => dest.IsCanRepayment, opt => opt.MapFrom(src => src.OrderStatus == OrderStatus.Pending && (src.VnPayInformation == null || src.VnPayInformation.PaymentStatus == PaymentStatus.UnPaid && src.VnPayInformation.ExpireAt <= DateTime.UtcNow)))
                                        .ForMember(dest => dest.PaymentUrl, opt => opt.MapFrom(src => src.VnPayInformation != null ? src.VnPayInformation.PaymentUrl : string.Empty))
                                        .ForMember(dest => dest.IsUsed , opt => opt.MapFrom(src => src.IsUsed))
                                        .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
                                        .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
                                        .ForMember(dest => dest.QrCode,opt => opt.MapFrom(src => src.OrderStatus == OrderStatus.Finished ? src.QrCode : null))
                                        .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.OrderStatus == OrderStatus.Finished ? src.ReceiverName : null))
                                        .ForMember(dest => dest.ReceiverEmail, opt => opt.MapFrom(src => src.OrderStatus == OrderStatus.Finished ? src.ReceiverEmail : null))
                                        .ForMember(dest => dest.ReceiverPhoneNumber, opt => opt.MapFrom(src => src.OrderStatus == OrderStatus.Finished ? src.ReceiverPhoneNumber : null));

            //**********************************************Voucher MAPPER**************************************************************
            CreateMap<CreateVoucherRequest, Voucher>()
                                        .ForMember(dest => dest.InitQuantity, opt => opt.MapFrom(src => src.Quantity))
                                        .ForMember(dest => dest.AppliedQuantity, opt => opt.MapFrom(src => src.Quantity == null ? (int?)null : 0));

            CreateMap<UpdateVoucherRequest, Voucher>()
                            .ForMember(dest => dest.InitQuantity, opt => opt.MapFrom(src => src.Quantity));
            CreateMap<Voucher, VoucherDto>();

            CreateMap<Voucher, VoucherDiscoveryDto>()
                                        .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.AppliedQuantity != null && src.InitQuantity != null ?  src.AppliedQuantity < src.InitQuantity : true && DateTime.UtcNow > src.StartDate && DateTime.UtcNow < src.EndDate));
                                        

            CreateMap<Voucher, SearchVoucherDto>();
                                        
        }
    }
}
