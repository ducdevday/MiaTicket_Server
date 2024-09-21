using AutoMapper;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.Data.Entity;
using MiaTicket.DataAccess;
using MiaTicket.VNPay;
using MiaTicket.VNPay.Response;
using Microsoft.AspNetCore.Http;
using System.Net;
using MiaTicket.Data.Enum;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.Email;
using MiaTicket.Email.Template;
using MiaTicket.WebAPI.Constant;
using MiaTicket.Email.Model;
using System.Net.Sockets;
using MiaTicket.BussinessLogic.Util;
using CloudinaryDotNet.Actions;

namespace MiaTicket.BussinessLogic.Business
{
    public interface IVNPayInformationBusiness
    {
        VnPayInformation? CreatePayment(double totalPrice);
        Task<UpdatePaymentVnPayResponse> UpdatePaymentVnPay(UpdatePaymentVnPayRequest request);
        Task SendOrderResultMail(Order order);
    }

    public class VNPayInformationBusiness : IVNPayInformationBusiness { 
        private readonly IDataAccessFacade _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly VNPayService _vnPayService = VNPayService.GetInstance();
        private readonly EmailService _emailService = EmailService.GetInstance();

        public VNPayInformationBusiness(IDataAccessFacade context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public VnPayInformation? CreatePayment(double totalPrice)
        {
            var paymentInformation = _vnPayService.CreatePayment(_httpContextAccessor, totalPrice);
            if (paymentInformation == null) return null;
            var vnPayInformation = _mapper.Map<VnPayInformation>(paymentInformation);
            return vnPayInformation;
        }

        public async Task<UpdatePaymentVnPayResponse> UpdatePaymentVnPay(UpdatePaymentVnPayRequest request)
        {
            var validation = new UpdatePaymentVnPayValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new UpdatePaymentVnPayResponse(HttpStatusCode.BadRequest, validation.Message, null);

            var vnPayInformation = await _context.VNPayInformationData.GetVnPayInformation(x => x.TransactionCode == request.TransactionCode);

            if (vnPayInformation == null) return new UpdatePaymentVnPayResponse(HttpStatusCode.BadRequest, "Payment Information Not Found", null);

            // In case User Paid Success But Call This API AGAIN
            if (vnPayInformation.PaymentStatus == PaymentStatus.Paid) return new UpdatePaymentVnPayResponse(HttpStatusCode.OK, "Payment Succeed", _mapper.Map<VnPayInformationDto>(vnPayInformation));

            var queryResult = await _vnPayService.QueryPaymentAsync(_httpContextAccessor, request.TransactionCode, request.TransactionDate);

            if(queryResult == null) return new UpdatePaymentVnPayResponse(HttpStatusCode.BadRequest, "Payment Information Not Found", null);

            //Payment Success
            if (queryResult.VnpResponseCode == "00" && queryResult.VnpTransactionStatus == "00" && double.TryParse(queryResult.VnpAmount, out double totalPaid) && totalPaid/100 == vnPayInformation.TotalAmount)
            {
                vnPayInformation.PaymentStatus = PaymentStatus.Paid;
                vnPayInformation.Order.OrderStatus = OrderStatus.Finished;
            }
            //Payment Processing
            else if (queryResult.VnpTransactionNo == "01")
            {

            }
            //Payment Fail
            else
            {
                vnPayInformation.Order.OrderStatus = OrderStatus.Canceled;

                //Re-Update Quantity Of Ticket
                var ticketIds = vnPayInformation.Order.OrderTickets.Select(x =>x.TicketId);
                List<Ticket> tickets = await _context.TicketData.GetTickets(x => ticketIds.Contains(x.Id));
                foreach (var orderTicket in vnPayInformation.Order.OrderTickets)
                {
                    var ticket = tickets.FirstOrDefault(t => t.Id == orderTicket.TicketId);
                    if (ticket != null)
                    {
                        ticket.Quantity += orderTicket.Quantity;
                        await _context.TicketData.UpdateTicket(ticket);
                    }
                }
            }

            var dataResponse = _mapper.Map<VnPayInformationDto>(vnPayInformation);

            await _context.VNPayInformationData.UpdateVnPayInformation(vnPayInformation);
            await _context.Commit();

            if (dataResponse.OrderStatus == OrderStatus.Finished) { 
                await SendOrderResultMail(vnPayInformation.Order);
            }


            return new UpdatePaymentVnPayResponse(HttpStatusCode.OK, "Success", dataResponse);
        }

        public Task SendOrderResultMail(Order order) {
            var orderTicketTemplate = OrderResultEmailTemplate.GetTicketTemplate();
            string ticketsHtml = "";
            for (var i = 0; i < order.OrderTickets.Count; i++)
            {
                var ticket = order.OrderTickets[i];
                ticketsHtml += orderTicketTemplate
                                    .Replace("{{TicketNo}}", i.ToString())
                                    .Replace("{{TicketName}}", ticket.Name)
                                    .Replace("{{TicketQuantity}}", ticket.Quantity.ToString())
                                    .Replace("{{TicketPrice}}", FormaterUtil.FormatPrice(ticket.Price));
            }
            var subtotalPrice = order.OrderTickets.Sum(x => x.Price * x.Quantity);
            var discount = subtotalPrice - order.TotalPrice;
            var orderResultBody = OrderResultEmailTemplate.GetOrderResultEmailTemplate().Replace("{{EventName}}", order.EventName)
                                                                                        .Replace("{{QrCode}}", order.QrCode)
                                                                                        .Replace("{{SubTotalPrice}}", FormaterUtil.FormatPrice(subtotalPrice))
                                                                                        .Replace("{{Discount}}", FormaterUtil.FormatPrice(discount))
                                                                                        .Replace("{{TotalPrice}}", FormaterUtil.FormatPrice(order.TotalPrice))
                                                                                        .Replace("{{ReceiverName}}", order.ReceiverName)
                                                                                        .Replace("{{ReceiverEmail}}", order.ReceiverEmail)
                                                                                        .Replace("{{ReceiverPhone}}", order.ReceiverPhoneNumber)
                                                                                        .Replace("{{PaymentType}}", Enum.GetName(typeof(PaymentType), order.PaymentType))
                                                                                        .Replace("{{Currency}}", "VND")
                                                                                        .Replace("{{ShowTime}}", FormaterUtil.FormatDateTimeRange(FormaterUtil.ConvertUtcToLocal(order.ShowTime.ShowStartAt), FormaterUtil.ConvertUtcToLocal(order.ShowTime.ShowEndAt)))
                                                                                        .Replace("{{Address}}", $"{order.AddressName}, {order.AddressDetail}")
                                                                                        .Replace("{{TicketItems}}", ticketsHtml);
                                                                                        
            var orderResultEmail = new OrderEmail()
            {
                Sender = "MiaTicket@email.com",
                Receiver = order.ReceiverEmail,
                Body = orderResultBody,
                Subject = "<MiaTicket>Order successful notification"
            };
            _emailService.Push(orderResultEmail);

            return Task.CompletedTask;
        }
    }
}
