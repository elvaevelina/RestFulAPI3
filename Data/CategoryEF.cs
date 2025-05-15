using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class CategoryEF : Icategory
    {
        // ef context
        private readonly ApplicationDbContext _context;
        public CategoryEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category AddCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding category", ex);
            }
        }

        public void DeleteCategory(int CategoryId)
        {
            var category = GetCategoryById(CategoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting category", ex);
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            // var categories = _context.Categories.OrderByDescending(categories => categories.CategoryId).ToList();
            var categories = from c in _context.Categories
                            orderby c.CategoryId descending
                            select c;
            return categories;
        }

        public Category GetCategoryById(int CategoryId)
        {
            // var category = _context.Categories.FirstOrDefault(c => c.CategoryId == CategoryId);
            var category = (from c in _context.Categories
                            where c.CategoryId == CategoryId
                            select c).FirstOrDefault();
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            var existingCategory = GetCategoryById(category.CategoryId);
            if(existingCategory == null)
            {
                throw new Exception("Category not found");
            }
            try
            {
                existingCategory.CategoryName = category.CategoryName;
                _context.Categories.Update(existingCategory);
                _context.SaveChanges();
                return existingCategory;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating category", ex);
            }
        }
    }
}