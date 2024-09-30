using AutoMapper;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Util;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using MiaTicket.DataAccess;
using MiaTicket.Email;
using MiaTicket.Email.Model;
using MiaTicket.Email.Template;
using MiaTicket.VNPay;
using MiaTicket.ZaloPay;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Business
{
    public interface IZaloPayInformationBusiness
    {
        Task<ZaloPayInformation?> CreatePayment(double totalPrice);
        Task<UpdatePaymentZaloPayResponse> UpdatePayment(UpdatePaymentZaloPayRequest request);
    }

    public class ZaloPayInformationBusiness : IZaloPayInformationBusiness
    {
        private readonly IDataAccessFacade _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IZaloPayService _zaloPayService;
        private readonly EmailService _emailService = EmailService.GetInstance();

        public ZaloPayInformationBusiness(IDataAccessFacade context, IHttpContextAccessor httpContextAccessor, IMapper mapper, IZaloPayService zaloPayService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _zaloPayService = zaloPayService;
        }
        public async Task<ZaloPayInformation?> CreatePayment(double totalPrice)
        {
            var paymentInformation = await _zaloPayService.CreatePayment(totalPrice);
            if (paymentInformation == null) return null;
            var zaloPayInfomation = _mapper.Map<ZaloPayInformation>(paymentInformation);
            return zaloPayInfomation;
        }

        public async Task<UpdatePaymentZaloPayResponse> UpdatePayment(UpdatePaymentZaloPayRequest request)
        {
            var validation = new UpdatePaymentZaloPayValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new UpdatePaymentZaloPayResponse(HttpStatusCode.BadRequest, validation.Message, null);

            var zaloPayInformation = await _context.ZaloPayInformationData.GetZaloPayInformation(x => x.TransactionCode == request.TransactionCode);

            if (zaloPayInformation == null) return new UpdatePaymentZaloPayResponse(HttpStatusCode.BadRequest, "Payment Information Not Found", null);

            // In case User Paid Success But Call This API AGAIN
            if (zaloPayInformation.PaymentStatus == PaymentStatus.Paid) return new UpdatePaymentZaloPayResponse(HttpStatusCode.OK, "Payment Succeed", _mapper.Map<ZaloPayInformationDto>(zaloPayInformation));

            var queryResult = await _zaloPayService.QueryPaymentAsync(request.TransactionCode);

            if (queryResult == null) return new UpdatePaymentZaloPayResponse(HttpStatusCode.BadRequest, "Payment Information Not Found", null);

            if (queryResult.IsProcessing)
            {

            }
            else if (queryResult.ReturnCode == 1 && queryResult.Amount == (long)zaloPayInformation.TotalAmount) { 
                zaloPayInformation.PaymentStatus = PaymentStatus.Paid;
                zaloPayInformation.Order.OrderStatus = OrderStatus.Finished;
            }
            else
            {
                zaloPayInformation.Order.OrderStatus = OrderStatus.Canceled;

                //Re-Update Quantity Of Ticket
                var ticketIds = zaloPayInformation.Order.OrderTickets.Select(x => x.TicketId);
                List<Ticket> tickets = await _context.TicketData.GetTickets(x => ticketIds.Contains(x.Id));
                foreach (var orderTicket in zaloPayInformation.Order.OrderTickets)
                {
                    var ticket = tickets.FirstOrDefault(t => t.Id == orderTicket.TicketId);
                    if (ticket != null)
                    {
                        ticket.Quantity += orderTicket.Quantity;
                        await _context.TicketData.UpdateTicket(ticket);
                    }
                }
            }

            var dataResponse = _mapper.Map<ZaloPayInformationDto>(zaloPayInformation);

            await _context.ZaloPayInformationData.UpdateZaloPayInformation(zaloPayInformation);
            await _context.Commit();

            if (dataResponse.OrderStatus == OrderStatus.Finished)
            {
                await SendOrderResultMail(zaloPayInformation.Order);
            }
            return new UpdatePaymentZaloPayResponse(HttpStatusCode.OK, "Success", dataResponse);
        }

        public Task SendOrderResultMail(Order order)
        {
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
