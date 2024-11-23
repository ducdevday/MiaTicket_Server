using AutoMapper;
using ClosedXML.Excel;
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
using System.Data;
using Microsoft.AspNetCore.Mvc;
namespace MiaTicket.BussinessLogic.Business
{
    public interface IOrderBusiness
    {
        Task<CreateOrderResponse> CreateOrder(Guid userId, CreateOrderRequest request);
        Task<GetMyOrdersResponse> GetMyOrders(Guid userId, GetMyOrdersRequest request);
        Task<GetOrderDetailResponse> GetOrderDetail(Guid userId, int orderId);
        Task<CancelOrderResponse> CancelOrder(Guid userId, int orderId);
        Task<GetOrderReportResponse> GetOrderReport(Guid userId, int eventId, GetOrderReportRequest request);
        Task<ExportOrderReportResponse> ExportOrderReport(Guid userId, int eventId, ExportOrderReportRequest request);
    }

    public class OrderBusiness : IOrderBusiness
    {
        private readonly IDataAccessFacade _context;
        private readonly IPaymentBusiness _paymentBusiness;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderCancellationService _orderCancellationService;
        public OrderBusiness(IDataAccessFacade context, IPaymentBusiness paymentBusiness, IMapper mapper, IHttpContextAccessor httpContextAccessor, IOrderCancellationService orderCancellationService)
        {
            _context = context;
            _paymentBusiness = paymentBusiness;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _orderCancellationService = orderCancellationService;
        }

        public async Task<CreateOrderResponse> CreateOrder(Guid userId, CreateOrderRequest request)
        {
            var validation = new CreateOrderValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new CreateOrderResponse(HttpStatusCode.BadRequest, validation.Message, string.Empty);

            var selectedEvent = await _context.EventData.GetEventBooking(request.EventId, request.ShowTimeId, request.OrderTickets.Select(x => x.TicketId).ToList());
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
                var vnPayInformation = _paymentBusiness.CreateVnPayPayment(totalPrice);
                if (vnPayInformation != null)
                {
                    paymentUrl = vnPayInformation.PaymentUrl;
                    order.Payment = vnPayInformation;
                }
                else
                {
                    return new CreateOrderResponse(HttpStatusCode.InternalServerError, "VnPay Payment GateWay Error", string.Empty);
                }
            }
            else if (request.PaymentType == PaymentType.ZaloPay) {
                var zaloPayInformation = await _paymentBusiness.CreateZaloPayPayment(totalPrice);
                if (zaloPayInformation != null)
                {
                    paymentUrl = zaloPayInformation.PaymentUrl;
                    order.Payment = zaloPayInformation;
                }
                else {
                    return new CreateOrderResponse(HttpStatusCode.InternalServerError, "ZaloPay Payment GateWay Error", string.Empty);
                }
            }

            var eventCheckIn = new EventCheckIn
            {
                Order = order,
            };
            order.EventCheckIn = eventCheckIn;

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

            if (order.OrderStatus != OrderStatus.Pending || order?.Payment?.PaymentStatus == PaymentStatus.Paid)
            {
                return new CancelOrderResponse(HttpStatusCode.Conflict, "Cannot Cancel Order", false);
            }

            order!.OrderStatus = OrderStatus.Canceled;
            var updatedOrder = await _context.OrderData.UpdateOrder(order);
            await _context.Commit();
            return new CancelOrderResponse(HttpStatusCode.OK, "Cancel Detail Success", true);
        }

        public async Task<GetOrderReportResponse> GetOrderReport(Guid userId, int eventId, GetOrderReportRequest request)
        {
            var validation = new GetOrderReportValidation(request);
            validation.Validate();
            if (!validation.IsValid) {
                return new GetOrderReportResponse(HttpStatusCode.BadRequest, validation.Message, []);
            }

            var eventOrganizer = await _context.EventOrganizerData.GetEventOrganizerById(eventId, userId);
            if (eventOrganizer == null)
            {
                return new GetOrderReportResponse(HttpStatusCode.NotFound, "Event Or User Invalid", []);
            }

            var organizerDto = _mapper.Map<MemberDto>(eventOrganizer);
            if (!CheckAbleToGetOrderReport(organizerDto))
            {
                return new GetOrderReportResponse(HttpStatusCode.Forbidden, "No Permission", []);
            }

            List<Order> orders = await _context.OrderData.GetOrderReport(eventId, request.ShowTimeId, request.PageIndex, request.PageSize, out int totalPages);
            var ordersReportDto = _mapper.Map<List<OrderReportDto>>(orders);
            return new GetOrderReportResponse(HttpStatusCode.Accepted, "Get Order Report Success", ordersReportDto, totalPages);
        }
        public async Task<ExportOrderReportResponse> ExportOrderReport(Guid userId, int eventId, ExportOrderReportRequest request)
        {
            var eventOrganizer = await _context.EventOrganizerData.GetEventOrganizerById(eventId, userId);
            if (eventOrganizer == null)
            {
                return new ExportOrderReportResponse(HttpStatusCode.NotFound, "Event Or User Invalid", null);
            }

            var organizerDto = _mapper.Map<MemberDto>(eventOrganizer);
            if (!CheckAbleToGetOrderReport(organizerDto))
            {
                return new ExportOrderReportResponse(HttpStatusCode.Forbidden, "No Permission", null);
            }

            List<Order> orders = await _context.OrderData.GetAllOrderReport(eventId, request.ShowTimeId);
            var ordersReportDto = _mapper.Map<List<OrderReportDto>>(orders);
            var orderTable = GetOrderDataTable(ordersReportDto);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var sheet1 = wb.AddWorksheet(orderTable, "Employee Records");

                sheet1.Row(1).Style.Font.FontColor = XLColor.White; 
                sheet1.Row(1).Style.Font.Bold = true;  
                

                sheet1.Column(1).Width = 15; 
                sheet1.Column(2).Width = 20; 
                sheet1.Column(3).Width = 30; 
                sheet1.Column(4).Width = 20;
                sheet1.Column(5).Width = 20;
                sheet1.Column(6).Width = 15; 
                sheet1.Column(7).Width = 20; 
                sheet1.Column(8).Width = 20; 

                sheet1.Column(7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                sheet1.Column(8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; 

                sheet1.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet1.Column(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                sheet1.Column(3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left; 
                sheet1.Column(4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; 
                sheet1.Column(5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; 
                sheet1.Column(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    ms.Seek(0, SeekOrigin.Begin);  // Ensure the memory stream is at the start

                    var fileContent = ms.ToArray();

                    return new ExportOrderReportResponse(HttpStatusCode.OK, "Export Order Report Result", fileContent);
                }
            }
        }


        private bool CheckAbleToGetOrderReport(MemberDto currentMemberDto)
        {
            switch (currentMemberDto.Role)
            {
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

        private DataTable GetOrderDataTable(List<OrderReportDto> orders)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Order Report";
            dt.Columns.Add("Order Id", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Phone Number", typeof(string));
            dt.Columns.Add("Payment Method", typeof(string));
            dt.Columns.Add("Tickets", typeof(string));
            dt.Columns.Add("Total Price (VND)", typeof(string));
            dt.Columns.Add("PaymentStatus", typeof(string));

            orders.ForEach(o =>
            {
                dt.Rows.Add(o.OrderId.ToString(),
                            o.ReceiverName, 
                            o.ReceiverEmail, 
                            o.ReceiverPhoneNumber, 
                            Enum.GetName(typeof(PaymentType), o.PaymentMethod), 
                            GetTicketFormatString(o.Tickets),
                            o.TotalPrice.ToString() ,
                            Enum.GetName(typeof(PaymentStatus), o.PaymentStatus));
            });
            return dt;
        }
        private string GetTicketFormatString(List<TicketReportDto> tickets) {
            return string.Join(",", tickets.Select(t => $"{t.Quantity} x {t.Name}"));
        }
    }
}
