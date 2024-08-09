using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountBusiness _context;

        public AccountController(IAccountBusiness context)
        {
            _context = context;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request) {
            var result = await _context.Register(request);
            HttpContext.Response.StatusCode = result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) { 
            var result = await _context.Login(request);
            HttpContext.Response.StatusCode = result.StatusCode;
            return new JsonResult(result);
        }
        
        //[HttpPut]
        //public async Task<IActionResult> DeActive() {

        //}
    }
}








