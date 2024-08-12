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
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request) {
            var result = await _context.CreateAccount(request);
            HttpContext.Response.StatusCode = (int) result.StatusCode;
            return new JsonResult(result);
        }
    }
}








