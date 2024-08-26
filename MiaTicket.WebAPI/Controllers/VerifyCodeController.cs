using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/verify-code")]
    [ApiController]
    public class VerifyCodeController : ControllerBase
    {
        private readonly IVerifyCodeBusiness _context;
        public VerifyCodeController(IVerifyCodeBusiness context)
        {
            _context = context;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendVerifyCode([FromBody] SendVerifyCodeRequest request)
        {
            var result = await _context.SendVerifyCode(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPatch("use")]
        public async Task<IActionResult> UseVerifyCode([FromBody] UseVerifyCodeRequest request) {
            var result = await _context.UseVerifyCode(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
    }
}
