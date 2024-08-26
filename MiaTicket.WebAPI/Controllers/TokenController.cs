using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.WebAPI.Constant;
using Microsoft.AspNetCore.Http;
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
            if (result.Data != null)
                HttpContext.Response.Cookies.Append("refreshToken", result.Data.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Path = "/",
                    SameSite = SameSiteMode.None,
                    MaxAge = TimeSpan.FromDays(AppConstant.REFRESH_TOKEN_EXPIRE_IN_DAYS)
                });
            var formattedResult = new
            {
                StatusCode = result.StatusCode,
                Message = result.Message,
                Data = new
                {
                    AccessToken = result?.Data?.AccessToken,
                }
            };
            return new JsonResult(formattedResult);
        }
    } 
}
