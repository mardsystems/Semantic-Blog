using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Business.Articles
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Article> Articles { get; set; }

        private readonly IArticlesQuery articlesQuery;

        public IndexModel(IArticlesQuery articlesQuery)
        {
            this.articlesQuery = articlesQuery;
        }

        public void OnGet(ArticlesQueryRequest request)
        {
            Articles = articlesQuery.GetArticles(request);
        }
    }
}
