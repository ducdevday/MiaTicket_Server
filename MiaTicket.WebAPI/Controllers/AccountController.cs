using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Entity;
using MiaTicket.WebAPI.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request) {
            var result = await _context.CreateAccount(request);
            HttpContext.Response.StatusCode = (int) result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) {
            var result = await _context.Login(request);
            HttpContext.Response.StatusCode = (int) result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPost("[action]")]
        [UserAuthorize(RequireRoles = ["User", "PowerUser", "Admin"])]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request) { 
            var result = await _context.Logout(request);
            HttpContext.Response.StatusCode = (int) result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request) {
            var result = await _context.RefreshToken(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAccessToken()
        {
            var result = await _tokenBusiness.GenerateToken();
            HttpContext.Response.StatusCode = 200;
            return new JsonResult(result);
        }

        [HttpGet("example")]
        [UserAuthorize(RequireRoles = ["User", "PowerUser", "Admin"])]
        public async Task<IActionResult> Example()
        {
            return Ok();
        }
    }
}








