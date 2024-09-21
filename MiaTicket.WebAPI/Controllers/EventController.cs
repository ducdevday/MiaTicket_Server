using Azure.Core;
using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Enum;
using MiaTicket.WebAPI.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventBusiness _context;

        public EventController(IEventBusiness context)
        {
            _context = context;
        }
        [HttpPost()]
        public async Task<IActionResult> CreateEvent([FromForm] CreateEventRequest request)
        {
            var result = await _context.CreateEvent(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById([FromRoute] int id)
        {
            var result = await _context.GetEventById(id);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent([FromRoute] int id, [FromForm] UpdateEventRequest request)
        {
            var result = await _context.UpdateEvent(id, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            var result = await _context.DeleteEvent(id);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
        [HttpGet("my-events")]
        [UserAuthorize(RequireRoles = [Role.User])]
        public async Task<IActionResult> GetMyEvents([FromQuery] GetMyEventsRequest request)
        {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.GetMyEvents(userId, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
        [HttpGet("latest")]
        public async Task<IActionResult> GetLastestEvents([FromQuery] GetLatestEventsRequest request)
        {
            var result = await _context.GetLatestEvents(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingEvents([FromQuery] GetTrendingEventsRequest request)
        {
            var result = await _context.GetTrendingEvents(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
        [HttpGet("by-category")]
        public async Task<IActionResult> GetEventsByCategory([FromQuery] GetEventsByCategoryRequest request)
        {
            var result = await _context.GetEventsByCategory(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchEvent([FromQuery] SearchEventRequest request)
        {
            var result = await _context.SearchEvent(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetEventDetail([FromRoute] int id) {
            var result = await _context.GetEventDetail(id);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet("detail/{eventId}/showtime/{showTimeId}")]
        public async Task<IActionResult> GetEventBooking([FromRoute] int eventId, [FromRoute] int showTimeId, [FromQuery] List<int>? ticketIds)
        {
            var result = await _context.GetEventBooking(eventId, showTimeId, ticketIds);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
    }
}
