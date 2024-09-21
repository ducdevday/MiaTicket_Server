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

        private readonly IVNPayInformationBusiness _vnPayInformationBusiness;

        public PaymentController(IVNPayInformationBusiness vNPayInformationBusiness)
        {
            _vnPayInformationBusiness = vNPayInformationBusiness;
        }

        [HttpPatch("vnpay")]
        [UserAuthorize(RequireRoles = [Role.User])]
        public async Task<IActionResult> UpdateVnpayPayment(UpdatePaymentVnPayRequest request)
        {
            var result = await _vnPayInformationBusiness.UpdatePaymentVnPay(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
    }
}
