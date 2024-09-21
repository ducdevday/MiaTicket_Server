using AutoMapper;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Business
{
    public interface ICategoryBusiness
    {
        Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request);
        Task<GetCategoriesDiscoveryResponse> GetCategoryList();
        Task<UpdateCategoryResponse> UpdateCategory(int id, UpdateCategoryRequest request);
        Task<DeleteCategoryResponse> DeleteCategory(int id);
    }

    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly IDataAccessFacade _context;
        private readonly IMapper _mapper;

        public CategoryBusiness(IDataAccessFacade context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request)
        {
            var validation = new CreateCategoryValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new CreateCategoryResponse(HttpStatusCode.BadRequest, validation.Message, false);

            var isCategoryExist = await _context.CategoryData.IsExistCategory(request.Name);
            if (isCategoryExist) return new CreateCategoryResponse(HttpStatusCode.BadRequest, "Category Has Already Exist", false);

            var addedCategory = await _context.CategoryData.CreateCategory(request.Name);
            if (addedCategory == null) return new CreateCategoryResponse(HttpStatusCode.Conflict, "Create Category Failed", false);
            await _context.Commit();
            return new CreateCategoryResponse(HttpStatusCode.OK, "Create Category Successfully", true);
        }

        public async Task<GetCategoriesDiscoveryResponse> GetCategoryList()
        {
            var categories = await _context.CategoryData.GetCategoryList();
            if (categories == null)
            {
                return new GetCategoriesDiscoveryResponse(HttpStatusCode.Conflict, "Get Category List Failed", null);
            }
            var cateList = categories.Select(x => _mapper.Map<CategoryDiscoveryDto>(x)).ToList();
            return new GetCategoriesDiscoveryResponse(HttpStatusCode.OK, "Get Category List Success", cateList);
        }

        public async Task<UpdateCategoryResponse> UpdateCategory(int id, UpdateCategoryRequest request)
        {
            var validation = new UpdateCategoryValidation(request);
            validation.Validate();


            var category = await _context.CategoryData.GetCategoryById(id);
            if (category == null) return new UpdateCategoryResponse(HttpStatusCode.BadRequest, "Category Has Not Found", false);

            var isCategoryExist = await _context.CategoryData.IsExistCategory(request.Name);
            if (isCategoryExist) return new UpdateCategoryResponse(HttpStatusCode.BadRequest, "Category Has Already Exist", false);

            category.Name = request.Name;
            var updatedCategory = await _context.CategoryData.UpdateCategory(category);
            await _context.Commit();

            if (updatedCategory == null) return new UpdateCategoryResponse(HttpStatusCode.Conflict, "Category Update Failed", false);
            return new UpdateCategoryResponse(HttpStatusCode.OK, "Category Update Successfully", true);
        }

        public async Task<DeleteCategoryResponse> DeleteCategory(int id)
        {
            var category = await _context.CategoryData.GetCategoryById(id);
            if (category == null) return new DeleteCategoryResponse(HttpStatusCode.BadRequest, "Category Has Not Found", false);

            await _context.CategoryData.DeleteCategory(category);
            await _context.Commit();
            return new DeleteCategoryResponse(HttpStatusCode.OK, "Category Delete Successfully", true);

        }

    }
}
