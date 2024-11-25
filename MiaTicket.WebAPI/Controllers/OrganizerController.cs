using AutoMapper.Execution;
using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Enum;
using MiaTicket.WebAPI.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/organizer")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly IOrganizerBusiness _context;

        public OrganizerController(IOrganizerBusiness context)
        {
            _context = context;
        }

        [HttpGet("events/{eventId}/members")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> GetEventMembers([FromRoute] int eventId, [FromQuery] GetEventMembersRequest request) {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.GetEventMembers(userId,eventId, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPost("events/{eventId}/members")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> AddEventMember([FromRoute] int eventId, [FromBody] AddEventMemberRequest request) {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.AddEventMember(userId, eventId, request);
            HttpContext.Response.StatusCode= (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPatch("events/{eventId}/members/{memberId}")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> UpdateEventMember([FromRoute] int eventId,[FromRoute] Guid memberId, [FromBody] UpdateEventMemberRequest request)
        {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.UpdateEventMember(userId, eventId, memberId, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpDelete("events/{eventId}/members/{memberId}")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> DeleteEventMember([FromRoute] int eventId, [FromRoute] Guid memberId) {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.DeleteEventMember(userId, eventId, memberId);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpPatch("events/{eventId}/checkin")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> CheckInEvent([FromRoute] int eventId, [FromBody] CheckInEventRequest request) {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.CheckInEvent(userId, eventId, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet("events/{eventId}/checkin")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> GetCheckInEventReport([FromRoute] int eventId, [FromQuery] GetCheckInEventReportRequest request)
        {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.GetCheckInEventReport(userId, eventId, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

    }
}
