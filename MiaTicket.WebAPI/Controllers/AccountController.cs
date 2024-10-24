using Azure.Core;
using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using MiaTicket.Setting;
using MiaTicket.WebAPI.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountBusiness _context;
        private readonly ITokenBusiness _tokenBusiness;

        public AccountController(IAccountBusiness context, ITokenBusiness tokenBusiness)
        {
            _context = context;
            _tokenBusiness = tokenBusiness;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            var result = await _context.CreateAccount(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _context.Login(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;

            var refreshToken = HttpContext.Items["refreshToken"]?.ToString();

            if (refreshToken != null)
            {
                HttpContext.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Path = "/",
                    SameSite = SameSiteMode.None,
                    MaxAge = TimeSpan.FromDays(AppConstant.REFRESH_TOKEN_EXPIRE_IN_DAYS)
                });
            }

            return new JsonResult(result);
        }

        [HttpPatch("activate")]
        public async Task<IActionResult> ActivateAccount([FromBody] ActivateAccountRequest request)
        {
            var result = await _context.ActivateAccount(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPost("logout")]
        [UserAuthorize(RequireRoles = [Role.Customer, Role.Organizer])]
        public async Task<IActionResult> Logout()
        {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var refreshToken = HttpContext.Request.Cookies["refreshToken"];
            var request = new LogoutRequest(userId, refreshToken);
            var result = await _context.Logout(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            HttpContext.Response.Cookies.Append("refreshToken", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Path = "/",
                SameSite = SameSiteMode.None,
                MaxAge = TimeSpan.Zero
            });
            return new JsonResult(result);
        }

        [HttpPatch("change-password")]
        [UserAuthorize(RequireRoles = [Role.Customer, Role.Organizer])]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var result = await _context.ChangePassword(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
        [HttpPatch("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _context.ResetPassword(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPut("{id}")]
        [UserAuthorize(RequireRoles = [Role.Customer, Role.Organizer])]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id, [FromForm] UpdateAccountRequest request)
        {
            var result = await _context.UpdateAccount(id, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet("information")]
        [UserAuthorize(RequireRoles = [Role.Customer, Role.Organizer])]
        public async Task<IActionResult> GetAccountInformation() {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.GetAccountInformation(userId);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

    }
}








