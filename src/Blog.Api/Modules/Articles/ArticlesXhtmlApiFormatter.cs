using System.Linq;
using System.Net.Http.Xhtml.Api;

namespace Blog.Modules.Articles
{
    public class ArticlesXhtmlApiFormatter : IArticlesApiFormatter
    {
        public object Format(Article[] articles)
        {
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

            return resource;
        }

        public object Format(ArticlesQueryRequest request)
        {

            var resource = new ResourceForm<ArticlesQueryRequest>
            {
                Title = "Articles Query",
                HRef = "/articles/query",
                Data = request,
                Method = "GET",
                Action = "/articles",
                Links = new Link[] { }
            };

            return resource;
        }

        public object FormatDetails(Article article)
        {
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

            return resource;
        }

        public object FormatPosting(ArticlePostingRequest request)
        {
            var resource = new ResourceForm<ArticlePostingRequest>
            {
                Title = "Articles Posting",
                HRef = "/articles/posting",
                Data = request,
                Method = "POST",
                Action = "/articles/posting",
                Links = new Link[] { }
            };

            return resource;
        }

        public object FormatPosting(Article article)
        {
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

            return resource;
        }
    }
}
