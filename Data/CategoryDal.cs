using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class CategoryDal : Icategory
    {
        
        private List<Category> _category =  new List<Category>();

        public CategoryDal()
        {
            _category=new List<Category>
            {
                new Category{CategoryId=1, CategoryName="ASP.NET Core"},
                new Category{CategoryId=1, CategoryName="ASP.NET MVC"},
                new Category{CategoryId=1, CategoryName="ASP.NET Web API"},
                new Category{CategoryId=1, CategoryName="Blazor"},
                new Category{CategoryId=1, CategoryName="Xamarin"},
                new Category{CategoryId=1, CategoryName="Azure"}
            };
        }
        public Category AddCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public Category UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}