using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Modules.Categories
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Category> Categories { get; set; }

        private readonly ICategoriesQuery categoriesQuery;

        public IndexModel(ICategoriesQuery categoriesQuery)
        {
            this.categoriesQuery = categoriesQuery;
        }

        public void OnGet()
        {
            Categories = categoriesQuery.GetCategories();
        }
    }
}
