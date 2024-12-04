using CloudinaryDotNet.Actions;
using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Enum;
using MiaTicket.WebAPI.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBusiness _context;

        public AdminController(IAdminBusiness context)
        {
            _context = context;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginRequest request) {
            var result = await _context.Login(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPatch("/change-default-password")]
        public async Task<IActionResult> ChangeDefaultPassword([FromBody] AdminChangeDefaultPasswordRequest request) {
            var result = await _context.ChangeDefaultPassword(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
    }
}
