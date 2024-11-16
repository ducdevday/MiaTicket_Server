using AutoMapper;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Entity;
using MiaTicket.VNPay.Model;
using MiaTicket.Data.Enum;
using MiaTicket.ZaloPay.Model;
using MiaTicket.BussinessLogic.Util;

namespace MiaTicket.BussinessLogic.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //**********************************************ACCOUNT MAPPER***********************************************************
            CreateMap<CreateAccountRequest, User>()
                                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid()))
                                        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => PasswordUtil.HashPassword(src.Password)));
            CreateMap<UpdateAccountRequest, User>();

            CreateMap<User, UserDto>();

            //**********************************************EVENT MAPPER***********************************************************
            //---------------------------------------------------------------------------------------------------------------------

            CreateMap<CreateEventRequest, Event>()
                                                    .ForMember(dest => dest.BankAccount, opt => opt.MapFrom(src => new BankAccount
                                                    {
                                                        BankNumber = src.PaymentNumber,
                                                        OwnerName = src.PaymentAccount,
                                                        BankName = src.PaymentBankName,
                                                        BankBranch = src.PaymentBankBranch
                                                    }))
                                                    .ForMember(dest => dest.EventOrganizers, opt => opt.MapFrom(src => new List<EventOrganizer>
                                                    {
                                                        new EventOrganizer
                                                        {
                                                            OrganizerId = src.UserId,
                                                            Position = OrganizerPosition.Owner 
                                                        }
                                                    }));
            CreateMap<UpdateEventRequest, Event>();
            CreateMap<ShowTimeDto, ShowTime>();
            CreateMap<TicketDto, Ticket>();

            CreateMap<Event, GetEventDataResponse>()
                                        .ForMember(dest => dest.PaymentNumber, opt => opt.MapFrom(src => src.BankAccount.BankNumber))
                                        .ForMember(dest => dest.PaymentAccount, opt => opt.MapFrom(src => src.BankAccount.OwnerName))
                                        .ForMember(dest => dest.PaymentBankName, opt => opt.MapFrom(src => src.BankAccount.BankName))
                                        .ForMember(dest => dest.PaymentBankBranch, opt => opt.MapFrom(src => src.BankAccount.BankBranch));

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

            CreateMap<CreateVnPayPaymentResult, Payment>();
            CreateMap<CreateZaloPayPaymentResult, Payment>();
            CreateMap<Payment, PaymentDto>().ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Order.OrderStatus));

            CreateMap<OrderTicket, OrderTicketDetailDto>()
                                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                                        .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                                        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
            CreateMap<Order, MyOrderDto>()
                                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                                        .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Event.BackgroundUrl))
                                        .ForMember(dest => dest.ShowTimeStart, opt => opt.MapFrom(src => src.ShowTime.ShowStartAt))
                                        .ForMember(dest => dest.ShowTimeEnd, opt => opt.MapFrom(src => src.ShowTime.ShowEndAt))
                                        .ForMember(dest => dest.AddressName, opt => opt.MapFrom(src => src.Event.AddressName))
                                        .ForMember(dest => dest.AddressDetail, opt => opt.MapFrom(src => FormaterUtil.FormatAddress(src.Event.AddressNo, src.Event.AddressWard, src.Event.AddressDistrict, src.Event.AddressProvince)))
                                        .ForMember(dest => dest.OrderTickets, opt => opt.MapFrom(src => src.OrderTickets))
                                        .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                                        .ForMember(dest => dest.IsCanCancel, opt => opt.MapFrom(src => src.OrderStatus == OrderStatus.Pending && src.Payment.PaymentStatus == PaymentStatus.UnPaid))
                                        .ForMember(dest => dest.IsCanRepayment, opt => opt.MapFrom(src => src.OrderStatus == OrderStatus.Pending && (src.Payment == null || src.Payment.PaymentStatus == PaymentStatus.UnPaid && DateTime.UtcNow <= src.Payment.ExpireAt)))
                                        .ForMember(dest => dest.PaymentUrl, opt => opt.MapFrom(src => src.Payment != null ? src.Payment.PaymentUrl : string.Empty));

            CreateMap<Order, OrderDetailDto>()
                                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                                        .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Event.BackgroundUrl))
                                        .ForMember(dest => dest.ShowTimeStart, opt => opt.MapFrom(src => src.ShowTime.ShowStartAt))
                                        .ForMember(dest => dest.ShowTimeEnd, opt => opt.MapFrom(src => src.ShowTime.ShowEndAt))
                                        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                                        .ForMember(dest => dest.AddressName, opt => opt.MapFrom(src => src.Event.AddressName))
                                        .ForMember(dest => dest.AddressDetail, opt => opt.MapFrom(src => FormaterUtil.FormatAddress(src.Event.AddressNo, src.Event.AddressWard, src.Event.AddressDistrict, src.Event.AddressProvince)))
                                        .ForMember(dest => dest.OrderTickets, opt => opt.MapFrom(src => src.OrderTickets))
                                        .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => (src.OrderTickets.Sum(x => x.Price * x.Quantity) - src.TotalPrice) > 0 ? (src.OrderTickets.Sum(x => x.Price * x.Quantity) - src.TotalPrice) : 0))
                                        .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                                        .ForMember(dest => dest.IsCanCancel, opt => opt.MapFrom(src => src.OrderStatus == OrderStatus.Pending && (src.Payment == null || src.Payment.PaymentStatus == PaymentStatus.UnPaid)))
                                        .ForMember(dest => dest.IsCanRepayment, opt => opt.MapFrom(src => src.OrderStatus == OrderStatus.Pending && (src.Payment == null || src.Payment.PaymentStatus == PaymentStatus.UnPaid && src.Payment.ExpireAt <= DateTime.UtcNow)))
                                        .ForMember(dest => dest.PaymentUrl, opt => opt.MapFrom(src => src.Payment != null ? src.Payment.PaymentUrl : string.Empty))
                                        .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.Payment.PaymentType))
                                        .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
                                        .ForMember(dest => dest.QrCode, opt => opt.MapFrom(src => src.OrderStatus == OrderStatus.Finished ? src.QrCode : null))
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
                                        .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.AppliedQuantity != null && src.InitQuantity != null ? src.AppliedQuantity < src.InitQuantity : true && DateTime.UtcNow > src.StartDate && DateTime.UtcNow < src.EndDate));


            CreateMap<Voucher, SearchVoucherDto>();

            //**********************************************Event Organizer MAPPER**************************************************************
            CreateMap<EventOrganizer, MemberDto>()
                                        .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.OrganizerId))
                                        .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Organizer.Name))
                                        .ForMember(dest => dest.MemberEmail, opt => opt.MapFrom(src => src.Organizer.Email))
                                        .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Position));

        }
    }
}
