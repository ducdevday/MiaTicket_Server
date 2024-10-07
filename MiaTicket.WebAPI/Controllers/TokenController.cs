using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using Microsoft.AspNetCore.Mvc;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenBusiness _context;
        public TokenController(ITokenBusiness context)
        {
            _context = context;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> GenerateToken()
        {
            var refreshToken = HttpContext.Request.Cookies["refreshToken"];
            var result = await _context.GenerateToken(new GenerateTokenRequest() { RefreshToken = refreshToken });
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
    } 
}
