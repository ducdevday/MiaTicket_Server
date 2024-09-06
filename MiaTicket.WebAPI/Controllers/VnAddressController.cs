using MiaTicket.BussinessLogic.Business;
using Microsoft.AspNetCore.Mvc;

namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class VnAddressController : ControllerBase
    {
        private readonly IVnAddressBusiness _context;

        public VnAddressController(IVnAddressBusiness context)
        {
            _context = context;
        }

        [HttpGet("provinces")]
        public async Task<IActionResult> GetProvinces()
        {
            var result = await _context.GetProvincesAsync();
            return new JsonResult(result);
        }

        [HttpGet("districts/{provinceId}")]
        public async Task<IActionResult> GetDisTricts([FromRoute] int provinceId) {
            var result = await _context.GetDisTrictsAsync(provinceId);
            return new JsonResult(result);
        }

        [HttpGet("wards/{districtId}")]
        public async Task<IActionResult> GetWards([FromRoute] int districtId) { 
            var result = await _context.GetWardsAsync(districtId);
            return new JsonResult(result);
        }
    }
}
