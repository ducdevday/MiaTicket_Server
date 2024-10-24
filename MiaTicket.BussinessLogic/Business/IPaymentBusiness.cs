using AutoMapper;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Util;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using MiaTicket.DataAccess;
using MiaTicket.Email.Model;
using MiaTicket.Email.Template;
using MiaTicket.Email;
using MiaTicket.VNPay;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MiaTicket.ZaloPay;

namespace MiaTicket.BussinessLogic.Business
{
    public interface IPaymentBusiness
    {
        Payment? CreateVnPayPayment(double totalPrice);

        Task<UpdatePaymentVnPayResponse> UpdateVnPayPayment(UpdatePaymentVnPayRequest request);
        Task<Payment?> CreateZaloPayPayment(double totalPrice);
        Task<UpdatePaymentZaloPayResponse> UpdateZaloPayPayment(UpdatePaymentZaloPayRequest request);
        Task SendOrderResultMail(Order order);
    }

    public class PaymentBusiness : IPaymentBusiness
    {
        private readonly IDataAccessFacade _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IVNPayService _vnPayService;
        private readonly IZaloPayService _zaloPayService;
        private readonly IEmailProducer _emailProducer;

        public PaymentBusiness(IDataAccessFacade context, IHttpContextAccessor httpContextAccessor, IMapper mapper, IVNPayService vnPayService,IZaloPayService zaloPayService, IEmailProducer emailProducer)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _vnPayService = vnPayService;
            _zaloPayService = zaloPayService;
            _emailProducer = emailProducer;
        }

        public Payment? CreateVnPayPayment(double totalPrice)
        {
            var vnPayInformation = _vnPayService.CreatePayment(totalPrice);
            if (vnPayInformation == null) return null;
            var paymentInformation = _mapper.Map<Payment>(vnPayInformation);
            paymentInformation.PaymentType = PaymentType.VnPay;
            return paymentInformation;
        }

        public async Task<Payment?> CreateZaloPayPayment(double totalPrice)
        {
            var zaloPayInfomation = await _zaloPayService.CreatePayment(totalPrice);
            if (zaloPayInfomation == null) return null;
            var paymentInformation = _mapper.Map<Payment>(zaloPayInfomation);
            paymentInformation.PaymentType = PaymentType.ZaloPay;
            return paymentInformation;
        }

        public async Task<UpdatePaymentVnPayResponse> UpdateVnPayPayment(UpdatePaymentVnPayRequest request)
        {
            var validation = new UpdatePaymentVnPayValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new UpdatePaymentVnPayResponse(HttpStatusCode.BadRequest, validation.Message, null);

            var vnPayInformation = await _context.PaymentData.GetPayment(x => x.TransactionCode == request.TransactionCode);

            if (vnPayInformation == null) return new UpdatePaymentVnPayResponse(HttpStatusCode.BadRequest, "Payment Information Not Found", null);

            // In case User Paid Success But Call This API AGAIN
            if (vnPayInformation.PaymentStatus == PaymentStatus.Paid) return new UpdatePaymentVnPayResponse(HttpStatusCode.OK, "Payment Succeed", _mapper.Map<PaymentDto>(vnPayInformation));

            var queryResult = await _vnPayService.QueryPaymentAsync(request.TransactionCode, request.TransactionDate);

            if (queryResult == null) return new UpdatePaymentVnPayResponse(HttpStatusCode.BadRequest, "Payment Information Not Found", null);

            //Payment Success
            if (queryResult.VnpResponseCode == "00" && queryResult.VnpTransactionStatus == "00" && double.TryParse(queryResult.VnpAmount, out double totalPaid) && totalPaid / 100 == vnPayInformation.TotalAmount)
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
                var ticketIds = vnPayInformation.Order.OrderTickets.Select(x => x.TicketId);
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

            var dataResponse = _mapper.Map<PaymentDto>(vnPayInformation);

            await _context.PaymentData.UpdatePayment(vnPayInformation);
            await _context.Commit();

            if (dataResponse.OrderStatus == OrderStatus.Finished)
            {
                await SendOrderResultMail(vnPayInformation.Order);
            }

            return new UpdatePaymentVnPayResponse(HttpStatusCode.OK, "Success", dataResponse);
        }

        public async Task<UpdatePaymentZaloPayResponse> UpdateZaloPayPayment(UpdatePaymentZaloPayRequest request)
        {
            var validation = new UpdatePaymentZaloPayValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new UpdatePaymentZaloPayResponse(HttpStatusCode.BadRequest, validation.Message, null);

            var zaloPayInformation = await _context.PaymentData.GetPayment(x => x.TransactionCode == request.TransactionCode);

            if (zaloPayInformation == null) return new UpdatePaymentZaloPayResponse(HttpStatusCode.BadRequest, "Payment Information Not Found", null);

            // In case User Paid Success But Call This API AGAIN
            if (zaloPayInformation.PaymentStatus == PaymentStatus.Paid) return new UpdatePaymentZaloPayResponse(HttpStatusCode.OK, "Payment Succeed", _mapper.Map<PaymentDto>(zaloPayInformation));

            var queryResult = await _zaloPayService.QueryPaymentAsync(request.TransactionCode);

            if (queryResult == null) return new UpdatePaymentZaloPayResponse(HttpStatusCode.BadRequest, "Payment Information Not Found", null);

            if (queryResult.IsProcessing)
            {

            }
            else if (queryResult.ReturnCode == 1 && queryResult.Amount == (long)zaloPayInformation.TotalAmount)
            {
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

            var dataResponse = _mapper.Map<PaymentDto>(zaloPayInformation);

            await _context.PaymentData.UpdatePayment(zaloPayInformation);
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
            var orderResultBody = OrderResultEmailTemplate.GetOrderResultEmailTemplate().Replace("{{EventName}}", order.Event.Name)
                                                                                        .Replace("{{QrCode}}", order.QrCode)
                                                                                        .Replace("{{SubTotalPrice}}", FormaterUtil.FormatPrice(subtotalPrice))
                                                                                        .Replace("{{Discount}}", FormaterUtil.FormatPrice(discount))
                                                                                        .Replace("{{TotalPrice}}", FormaterUtil.FormatPrice(order.TotalPrice))
                                                                                        .Replace("{{ReceiverName}}", order.ReceiverName)
                                                                                        .Replace("{{ReceiverEmail}}", order.ReceiverEmail)
                                                                                        .Replace("{{ReceiverPhone}}", order.ReceiverPhoneNumber)
                                                                                        .Replace("{{PaymentType}}", Enum.GetName(typeof(PaymentType), order.Payment.PaymentType))
                                                                                        .Replace("{{Currency}}", "VND")
                                                                                        .Replace("{{ShowTime}}", FormaterUtil.FormatDateTimeRange(FormaterUtil.ConvertUtcToLocal(order.ShowTime.ShowStartAt), FormaterUtil.ConvertUtcToLocal(order.ShowTime.ShowEndAt)))
                                                                                        .Replace("{{Address}}", $"{order.Event.AddressName}, {order.Event.AddressNo}, {order.Event.AddressWard}, {order.Event.AddressDistrict}, {order.Event.AddressProvince}")
                                                                                        .Replace("{{TicketItems}}", ticketsHtml);

            var orderResultEmail = new EmailModel()
            {
                Sender = "MiaTicket@email.com",
                Receiver = order.ReceiverEmail,
                Body = orderResultBody,
                Subject = "<MiaTicket>Order successful notification"
            };

            _emailProducer.SendMessage(orderResultEmail);

            return Task.CompletedTask;
        }


    }
}
