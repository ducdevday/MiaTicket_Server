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
        private readonly IZaloPayInformationBusiness _zaloPayInformationBusiness;
        public PaymentController(IVNPayInformationBusiness vNPayInformationBusiness, IZaloPayInformationBusiness zaloPayInformationBusiness)
        {
            _vnPayInformationBusiness = vNPayInformationBusiness;
            _zaloPayInformationBusiness = zaloPayInformationBusiness;
        }

        [HttpPatch("vnpay")]
        [UserAuthorize(RequireRoles = [Role.User])]
        public async Task<IActionResult> UpdateVnpayPayment(UpdatePaymentVnPayRequest request)
        {
            var result = await _vnPayInformationBusiness.UpdatePayment(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPatch("zalopay")]
        [UserAuthorize(RequireRoles = [Role.User])]
        public async Task<IActionResult> UpdateZaloPayment(UpdatePaymentZaloPayRequest request) {
            var result = await _zaloPayInformationBusiness.UpdatePayment(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }


    }
}
