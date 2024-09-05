using Azure.Core;
using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/banner")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IBannerBusiness _context;

        public BannerController(IBannerBusiness context)
        {
            _context = context;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateBanner([FromForm] CreateBannerRequest request) {
            var result =await _context.CreateBanner(request);
            HttpContext.Response.StatusCode =(int) result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet("discovery")]
        public async Task<IActionResult> GetBannersDiscovery() {
            var result = await _context.GetBannersDiscovery();
            HttpContext.Response.StatusCode = (int) result.StatusCode;
            return new JsonResult(result);
        }
    }
}
