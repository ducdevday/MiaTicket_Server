using Azure.Core;
using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using MiaTicket.WebAPI.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _context;

        public OrderController(IOrderBusiness context)
        {
            _context = context;
        }

        [HttpPost("")]
        [UserAuthorize(RequireRoles = [Role.Customer])]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request) {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.CreateOrder(userId,request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet("my-orders")]
        [UserAuthorize(RequireRoles = [Role.Customer])]
        public async Task<IActionResult> GetMyOrders([FromQuery] GetMyOrdersRequest request) {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.GetMyOrders(userId, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet("detail/{id}")]
        [UserAuthorize(RequireRoles = [Role.Customer])]
        public async Task<IActionResult> GetOrderDetail([FromRoute] int id) {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.GetOrderDetail(userId, id);
            HttpContext.Response.StatusCode= (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPatch("cancel/{id}")]
        [UserAuthorize(RequireRoles = [Role.Customer])]
        public async Task<IActionResult> CancelOrder([FromRoute] int id)
        {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.CancelOrder(userId, id);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
    }
}
