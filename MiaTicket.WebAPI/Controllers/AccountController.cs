using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using MiaTicket.WebAPI.Constant;
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
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request) {
            var result = await _context.CreateAccount(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) {
            var result = await _context.Login(request);
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
                Data = result.Data != null ? new
                {
                    AccessToken = result.Data.AccessToken,
                    User = new
                    {
                        Id = result.Data.User.Id,
                        Name = result.Data.User.Name,
                        AvatarUrl = result.Data.User.AvatarUrl,
                        BirthDate = result.Data.User.BirthDate,
                        Email = result.Data.User.Email,
                        Gender = result.Data.User.Gender,
                        PhoneNumber = result.Data.User.PhoneNumber,
                    }
                } : null
            };

            return new JsonResult(formattedResult);
        }

        [HttpPatch("activate")]
        public async Task<IActionResult> ActivateAccount([FromBody] ActivateAccountRequest request)
        {
            var result = await _context.ActivateAccount(request);
            HttpContext.Response.StatusCode = (int) result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPost("logout")]
        [UserAuthorize(RequireRoles = [Role.User, Role.Admin])]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request) {
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
        [UserAuthorize(RequireRoles = [Role.User, Role.Admin])]
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
        [UserAuthorize(RequireRoles = [Role.User, Role.Admin])]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id,[FromForm] UpdateAccountRequest request)
        {
            var result = await _context.UpdateAccount(id,request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

    }
}








