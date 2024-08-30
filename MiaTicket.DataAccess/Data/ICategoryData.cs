using MiaTicket.Data;
using MiaTicket.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface ICategoryData
    {
        Task<Category?> GetCategoryById(int categoryId);
        Task<bool> IsExistCategory(string name);
        Task<bool> IsExistCategory(int Id);

        Task<Category> CreateCategory(string name);
        Task<Category> UpdateCategory(Category category);
        Task DeleteCategory(Category category);

        Task<List<Category>> GetCategoryList();
    }

    public class CategoryData : ICategoryData
    {
        private readonly MiaTicketDBContext _context;

        public CategoryData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<Category> CreateCategory(string name)
        {
            var cate = _context.Category.Add(new Category { Name = name });
            return Task.FromResult(cate.Entity);
        }

        public Task<bool> IsExistCategory(string name)
        {
            var cate =_context.Category.FirstOrDefault(c => c.Name == name);
            if (cate != null) {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        public Task<bool> IsExistCategory(int id)
        {
            var cate = _context.Category.Find(id);
            if (cate != null)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }


        public Task<Category?> GetCategoryById(int categoryId)
        {
            var category = _context.Category.Find(categoryId);

            return Task.FromResult(category);
        }


        public Task<List<Category>> GetCategoryList()
        {
            var categories = _context.Category.ToList() ;
            return Task.FromResult(categories);
        }

        public Task<Category> UpdateCategory(Category category)
        {
            var updateEntity = _context.Category.Update(category);
            return Task.FromResult(updateEntity.Entity);
        }

        public Task DeleteCategory(Category category)
        {
            _context.Category.Remove(category);
            return Task.CompletedTask;
        }


    }
}
