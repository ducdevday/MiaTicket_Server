using AutoMapper;
using MiaTicket.BussinessLogic.Factory;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using MiaTicket.DataAccess;
using MiaTicket.Schedular.Service;
using Microsoft.AspNetCore.Http;
using System.Net;
namespace MiaTicket.BussinessLogic.Business
{
    public interface IOrderBusiness
    {
        Task<CreateOrderResponse> CreateOrder(Guid userId, CreateOrderRequest request);
        Task<GetMyOrdersResponse> GetMyOrders(Guid userId, GetMyOrdersRequest request);
        Task<GetOrderDetailResponse> GetOrderDetail(Guid userId, int orderId);
        Task<CancelOrderResponse> CancelOrder(Guid userId, int orderId);
    }

    public class OrderBusiness : IOrderBusiness
    {
        private readonly IDataAccessFacade _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IVNPayInformationBusiness _vnPayBusiness;
        private readonly IZaloPayInformationBusiness _zaloPayBusiness;
        private readonly IOrderCancellationService _orderCancellationService;
        public OrderBusiness(IDataAccessFacade context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVNPayInformationBusiness vNPayBusiness, IZaloPayInformationBusiness zaloPayBusiness, IOrderCancellationService orderCancellationService)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _vnPayBusiness = vNPayBusiness;
            _zaloPayBusiness = zaloPayBusiness;
            _orderCancellationService = orderCancellationService;
        }

        public async Task<CreateOrderResponse> CreateOrder(Guid userId, CreateOrderRequest request)
        {
            var validation = new CreateOrderValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new CreateOrderResponse(HttpStatusCode.BadRequest, validation.Message, string.Empty);

            var selectedEvent = await _context.EventData.GetEventById(request.EventId, request.ShowTimeId, request.OrderTickets.Select(x => x.TicketId).ToList());
            if (selectedEvent == null) return new CreateOrderResponse(HttpStatusCode.Conflict, "Event or ShowTime not found", string.Empty);

            var selectedShowTime = selectedEvent.ShowTimes.FirstOrDefault();
            if (selectedShowTime == null) return new CreateOrderResponse(HttpStatusCode.Conflict, "ShowTime not found", string.Empty);

            var selectedTickets = selectedShowTime.Tickets;
            if (selectedTickets == null) return new CreateOrderResponse(HttpStatusCode.Conflict, "Tickets not available", string.Empty);

            if (DateTime.UtcNow < selectedShowTime.SaleStartAt || selectedShowTime.SaleEndAt < DateTime.UtcNow)
            {
                return new CreateOrderResponse(HttpStatusCode.Conflict, "Tickets are not in sale time", string.Empty);
            }

            foreach (var orderTicket in request.OrderTickets)
            {
                var ticket = selectedTickets.FirstOrDefault(x => x.Id == orderTicket.TicketId);
                if (ticket == null || orderTicket.Quantity > ticket.MaximumPurchase || orderTicket.Quantity < ticket.MinimumPurchase || orderTicket.Quantity > ticket.Quantity)
                    return new CreateOrderResponse(HttpStatusCode.Conflict, "Tickets have invalid quantity", string.Empty);
            }

            Voucher? voucher = null;
            if (request.VoucherId != null)
            {
                voucher = await _context.VoucherData.GetVoucherById((int)request.VoucherId);

                if(voucher != null){
                    if(voucher.EventId != request.EventId)
                            return new CreateOrderResponse(HttpStatusCode.Conflict, "Voucher Not Valid", string.Empty);
                    voucher.AppliedQuantity++;
                    var updatedVoucher = await _context.VoucherData.UpdateVoucher(voucher); 
                }
            }

            var order = _mapper.Map<Order>(request);
            order.EventName = selectedEvent.Name;
            order.AddressName = selectedEvent.AddressName ?? "";
            order.AddressDetail = $"{selectedEvent.AddressNo}, {selectedEvent.AddressWard}, {selectedEvent.AddressDistrict}, {selectedEvent.AddressProvince}";
            order.BackgroundUrl = selectedEvent.BackgroundUrl;
            order.LogoUrl = selectedEvent.LogoUrl;
            order.OrganizerName = selectedEvent.OrganizerName;
            order.OrganizerInformation = selectedEvent.OrganizerInformation;
            order.OrganizerLogoUrl = selectedEvent.OrganizerLogoUrl;
            order.DateStart = selectedShowTime.ShowStartAt;
            order.DateEnd = selectedShowTime.ShowEndAt;
            order.QrCode = GenerateQrCode(8);
            order.UserId = userId;

            foreach (var orderTicket in order.OrderTickets)
            {
                var ticket = selectedTickets.FirstOrDefault(t => t.Id == orderTicket.TicketId);
                if (ticket != null)
                {
                    orderTicket.Name = ticket.Name;
                    orderTicket.Price = ticket.Price;

                    ticket.Quantity -= orderTicket.Quantity;
                    await _context.TicketData.UpdateTicket(ticket);
                }
            }

            var pricingStrategy = OrderPricingFactory.GetPriceStragegy(voucher?.Type);
            var totalPrice = pricingStrategy.CalculateTotalPrice(order.OrderTickets, voucher?.Value ?? 0);
            order.TotalPrice = totalPrice;

            var paymentUrl = string.Empty;
            if (request.PaymentType == PaymentType.VnPay)
            {
                var vnPayInformation = _vnPayBusiness.CreatePayment(totalPrice);
                if (vnPayInformation != null)
                {
                    paymentUrl = vnPayInformation.PaymentUrl;
                    order.VnPayInformation = vnPayInformation;
                }
                else
                {
                    return new CreateOrderResponse(HttpStatusCode.InternalServerError, "VnPay Payment GateWay Error", string.Empty);
                }
            }
            else if (request.PaymentType == PaymentType.ZaloPay) {
                var zaloPayInformation = await _zaloPayBusiness.CreatePayment(totalPrice);
                if (zaloPayInformation != null)
                {
                    paymentUrl = zaloPayInformation.PaymentUrl;
                    order.ZaloPayInformation = zaloPayInformation;
                }
                else {
                    return new CreateOrderResponse(HttpStatusCode.InternalServerError, "ZaloPay Payment GateWay Error", string.Empty);
                }
            }

            await _context.OrderData.CreateOrder(order);
            await _context.Commit();

            await _orderCancellationService.ScheduleCancelOrderIfNotPaid(order.Id);
        
            return new CreateOrderResponse(HttpStatusCode.OK, "Order created successfully", paymentUrl);
        }


        public async Task<GetMyOrdersResponse> GetMyOrders(Guid userId, GetMyOrdersRequest request)
        {
            var validation = new GetMyOrdersValidation(request);
            validation.Validate();
            if (!validation.IsValid) {
                return new GetMyOrdersResponse(HttpStatusCode.BadRequest, validation.Message, []);
            }

            List<Order> orders = await _context.OrderData.GetOrders(userId, request.PageIndex, request.PageSize, request.Keyword, request.OrderStatus, out int totalPages);

            List<MyOrderDto> dataResponse = _mapper.Map<List<MyOrderDto>>(orders);

            return new GetMyOrdersResponse(HttpStatusCode.OK, "Get Orders Success", dataResponse, totalPages);

        }

        public string GenerateQrCode(int length = 8)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<GetOrderDetailResponse> GetOrderDetail(Guid userId, int orderId)
        {
            Order? order = await _context.OrderData.GetOrder(userId, orderId);
            if (order == null) {
                return new GetOrderDetailResponse(HttpStatusCode.Conflict, "Cannot Find Order", null);
            }

            OrderDetailDto orderDto = _mapper.Map<OrderDetailDto>(order);
            return new GetOrderDetailResponse(HttpStatusCode.OK, "Get Order Detail Success", orderDto);

        }

        public async Task<CancelOrderResponse> CancelOrder(Guid userId, int orderId)
        {
            Order? order = await _context.OrderData.GetOrder(userId, orderId);
            if (order == null)
            {
                return new CancelOrderResponse(HttpStatusCode.Conflict, "Cannot Find Order", false);
            }

            if (order.OrderStatus != OrderStatus.Pending || order?.VnPayInformation?.PaymentStatus == PaymentStatus.Paid)
            {
                return new CancelOrderResponse(HttpStatusCode.Conflict, "Cannot Cancel Order", false);
            }

            order!.OrderStatus = OrderStatus.Canceled;
            var updatedOrder = await _context.OrderData.UpdateOrder(order);
            return new CancelOrderResponse(HttpStatusCode.OK, "Cancel Detail Success", true);
        }
    }
}
