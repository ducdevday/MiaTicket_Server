using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/verification-code")]
    [ApiController]
    public class VerificationCodeController : ControllerBase
    {
        private readonly IVerificationCodeBusiness _context;
        public VerificationCodeController(IVerificationCodeBusiness context)
        {
            _context = context;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendVerificationCode([FromBody] SendVerificationCodeRequest request)
        {
            var result = await _context.SendVerificationCode(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPatch("use")]
        public async Task<IActionResult> UseVerificationCode([FromBody] UseVeriicationCodeRequest request) {
            var result = await _context.UseVerificationCode(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
    }
}
