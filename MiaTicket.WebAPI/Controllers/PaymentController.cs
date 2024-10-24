using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Enum;
using MiaTicket.WebAPI.Policy;
using Microsoft.AspNetCore.Mvc;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentBusiness _paymentBusiness;
        public PaymentController(IPaymentBusiness paymentBusiness)
        {
            _paymentBusiness = paymentBusiness;
        }

        [HttpPatch("vnpay")]
        [UserAuthorize(RequireRoles = [Role.Customer])]
        public async Task<IActionResult> UpdateVnpayPayment(UpdatePaymentVnPayRequest request)
        {
            var result = await _paymentBusiness.UpdateVnPayPayment(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPatch("zalopay")]
        [UserAuthorize(RequireRoles = [Role.Customer])]
        public async Task<IActionResult> UpdateZaloPayment(UpdatePaymentZaloPayRequest request) {
            var result = await _paymentBusiness.UpdateZaloPayPayment(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }


    }
}
