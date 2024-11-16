using Azure.Core;
using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Enum;
using MiaTicket.WebAPI.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/voucher")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherBusiness _context;

        public VoucherController(IVoucherBusiness context)
        {
            _context = context;
        }

        [HttpPost()]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> CreateVoucher([FromBody] CreateVoucherRequest request) {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.CreateVoucher(userId, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPut("{voucherId}")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> UpdateVoucher([FromRoute] int voucherId,[FromBody] UpdateVoucherRequest request) {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.UpdateVoucher(userId, voucherId, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpDelete("{voucherId}")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> DeleteVoucher([FromRoute] int voucherId)
        {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.DeleteVoucher(userId, voucherId);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet("my-vouchers/{eventId}")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> GetMyVouchers([FromRoute] int eventId, [FromQuery] string keyword = "")
        {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.GetMyVouchers(userId, eventId, keyword);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet("discovery/{eventId}")]
        public async Task<IActionResult> GetVouchersDiscovery([FromRoute] int eventId)
        {
            var result = await _context.GetVouchersDiscovery(eventId);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchVoucher([FromQuery] SearchVoucherRequest request) {
            var result = await _context.SearchVoucher(request);
            HttpContext.Response.StatusCode = (int) result.StatusCode;
            return new JsonResult(result);
        }
    }
}
