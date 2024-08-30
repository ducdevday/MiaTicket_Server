using MiaTicket.BussinessLogic.Business;
using MiaTicket.BussinessLogic.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace MiaTicket.WebAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBusiness _context;

        public CategoryController(ICategoryBusiness context)
        {
            _context = context;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var result =await _context.CreateCategory(request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetListCategory() {
            var result = await _context.GetCategoryList();
            HttpContext.Response.StatusCode= (int)result.StatusCode;
            return new JsonResult(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]int id, [FromBody]UpdateCategoryRequest request) {
            var result = await _context.UpdateCategory(id, request);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int id)
        {
            var result = await _context.DeleteCategory(id);
            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return new JsonResult(result);
        }
    }
}
