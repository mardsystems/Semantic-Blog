using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Web;

namespace Blog.Modules.Articles
{    
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesQuery articlesQuery;

        private readonly IArticlesPosting articlesPosting;

        public ArticlesController(IArticlesQuery articlesQuery, IArticlesPosting articlesPosting)
        {
            this.articlesQuery = articlesQuery;

            this.articlesPosting = articlesPosting;
        }

        [HttpGet]
        public IActionResult GetArticles(ArticlesQueryRequest request)
        {
            var articles = articlesQuery.GetArticles(request);

            var data = articles
                .Select(article => new Resource<Article>
                {
                    Title = $"Article #{article.Id}",
                    HRef = $"/articles/{article.Id}",
                    Data = article,
                    Links = new Link[]
                    {
                        new Link {Rel = "article-details", HRef = $"/articles/{article.Id}", Text = "Detail"},
                        new Link {Rel = "article-edit", HRef = $"/articles/{article.Id}/edit", Text = "Edit"},
                        new Link {Rel = "article-delete", HRef = $"/articles/{article.Id}/delete", Text = "Delete"}
                    }
                })
                .ToArray();

            var resource = new ResourceCollection<Article>
            {
                Title = "Articles",
                HRef = "/articles",
                Data = data,
                Links = new Link[]
                {
                    new Link {Rel = "articles-query", HRef = "/articles/query", Text = "Articles Query"},
                    new Link {Rel = "articles-posting", HRef = "/articles/posting", Text = "Articles Posting"}
                }
            };

            return Ok(resource);
        }

        [HttpGet("query")]
        public IActionResult GetArticleQuery()
        {
            var request = new ArticlesQueryRequest
            {
                Title = null,
            };

            var resource = new ResourceForm<ArticlesQueryRequest>
            {
                Title = "Articles Query",
                HRef = "/articles/query",
                Data = request,
                Method = "GET",
                Action = "/articles",
                Links = new Link[] { }
            };

            return Ok(resource);
        }

        [HttpGet("{id}")]
        public IActionResult GetArticleDetails(string id)
        {
            var articleId = new ArticleId(id);

            var article = articlesQuery.GetArticle(articleId);

            var resource = new Resource<Article>
            {
                Title = $"Article #{article.Id}",
                HRef = $"/articles/{article.Id}",
                Data = article,
                Links = new Link[]
                {
                    new Link {Rel = "article-edit", HRef = $"/articles/{article.Id}/edit", Text = "Edit"},
                    new Link {Rel = "article-delete", HRef = $"/articles/{article.Id}/delete", Text = "Delete"}
                }
            };

            return Ok(resource);
        }

        [HttpGet("posting")]
        public IActionResult GetArticlesPosting()
        {
            var request = new ArticlePostingRequest
            {
                //Id = Guid.NewGuid().ToString(),
                Title = "Title #"
            };

            var resource = new ResourceForm<ArticlePostingRequest>
            {
                Title = "Articles Posting",
                HRef = "/articles/posting",
                Data = request,
                Method = "POST",
                Action = "/articles/posting",
                Links = new Link[] { }
            };

            return Ok(resource);
        }

        [HttpPost("posting")]
        public IActionResult PostArticlesPosting(ArticlePostingRequest request)
        {
            var article = articlesPosting.PostArticle(request);

            var resource = new Resource<Article>
            {
                Title = $"Article #{article.Id}",
                HRef = $"/articles/{article.Id}",
                Data = article,
                Links = new Link[]
                {
                    new Link {Rel = "article-details", HRef = $"/articles/{article.Id}", Text = "Detail"},
                    new Link {Rel = "article-edit", HRef = $"/articles/{article.Id}/edit", Text = "Edit"},
                    new Link {Rel = "article-delete", HRef = $"/articles/{article.Id}/delete", Text = "Delete"}
                }
            };

            return CreatedAtAction(nameof(GetArticleDetails), new { id = article.Id }, resource);
        }
    }
}
