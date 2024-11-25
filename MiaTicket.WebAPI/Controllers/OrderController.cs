using Azure.Core;
using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using MiaTicket.WebAPI.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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
            var result = await _context.CreateOrder(userId, request);
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
            HttpContext.Response.StatusCode = (int)result.StatusCode;
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

        [HttpGet("events/{eventId}/report")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> GetOrderReport([FromRoute] int eventId, [FromQuery] GetOrderReportRequest request) {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.GetOrderReport(userId, eventId, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet("events/{eventId}/export-report")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> ExportOrderReport([FromRoute] int eventId, [FromQuery] ExportOrderReportRequest request)
        {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.ExportOrderReport(userId, eventId, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            if (result.Data != null) {
                // Trả về file như một phản hồi HTTP
                var fileName = $"OrderReport_{eventId}_{DateTime.UtcNow:yyyyMMddHHmmss}.xlsx";
                return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            return new JsonResult(result);
        }
        [HttpGet("events/{eventId}/summary-revenue")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> GetOrderSummaryRevenue([FromRoute] int eventId, [FromQuery] GetOrderSummaryRevenueRequest request) {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.GetOrderSummaryRevenue(userId, eventId, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
        [HttpGet("events/{eventId}/summary-figures")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> GetOrderSummaryFigure([FromRoute] int eventId, [FromQuery] GetOrderSummaryFigureRequest request) {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.GetOrderSummaryFigure(userId, eventId, request);
            HttpContext.Response.StatusCode= (int)result.StatusCode;
            return new JsonResult(result);
        }
    }
}
