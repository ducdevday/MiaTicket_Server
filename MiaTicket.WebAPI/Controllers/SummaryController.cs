using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.Data.Enum;
using MiaTicket.WebAPI.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/summary")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryBusiness _context;
        public SummaryController(ISummaryBusiness context) { 
            _context = context;
        }

        [HttpGet("event-categories")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> GetSummaryEventCategories() {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.GetSummaryEventCategories(userId);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet("directory-organizers")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> GetDirectoryOrganizers() { 
            _ = Guid.TryParse(User.FindFirst("id")?.Value,out Guid userId);
            var result = await _context.GetDirectoryOrganizers(userId);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet("event-participation-timeline")]
        [UserAuthorize(RequireRoles = [Role.Organizer])]
        public async Task<IActionResult> GetEventParticipationTimeline([FromQuery] GetEventParticipationTimelineRequest request) {
            _ = Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId);
            var result = await _context.GetEventParticipationTimeline(userId, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
    }
}
