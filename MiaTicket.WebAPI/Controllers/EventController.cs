using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var result = await _context.UpdateEvent(id ,request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

    }
}
