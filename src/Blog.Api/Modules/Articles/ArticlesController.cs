using Microsoft.AspNetCore.Mvc;

namespace Blog.Modules.Articles
{
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesQuery articlesQuery;

        private readonly IArticlesPosting articlesPosting;

        private readonly IArticlesApiFormatter articlesApiFormatter;

        public ArticlesController(
            IArticlesQuery articlesQuery,
            IArticlesPosting articlesPosting,
            IArticlesApiFormatter articlesApiFormatter)
        {
            this.articlesQuery = articlesQuery;

            this.articlesPosting = articlesPosting;

            this.articlesApiFormatter = articlesApiFormatter;
        }


        [HttpGet]
        public IActionResult GetArticles(string title)
        {
            var request = new ArticlesQueryRequest
            {
                Title = title,
            };

            var articles = articlesQuery.GetArticles(request);

            var output = articlesApiFormatter.Format(articles);

            return Ok(output);
        }

        [HttpGet("query")]
        public IActionResult GetArticleQuery()
        {
            var request = new ArticlesQueryRequest
            {
                Title = null,
            };

            var output = articlesApiFormatter.Format(request);

            return Ok(output);
        }

        [HttpGet("{id}")]
        public IActionResult GetArticleDetails(string id)
        {
            var articleId = new ArticleId(id);

            var article = articlesQuery.GetArticle(articleId);

            var output = articlesApiFormatter.FormatDetails(article);

            return Ok(output);
        }

        [HttpGet("posting")]
        public IActionResult GetArticlesPosting()
        {
            var request = new ArticlePostingRequest
            {
                //Id = Guid.NewGuid().ToString(),
                Title = "Title #"
            };

            var output = articlesApiFormatter.FormatPosting(request);


            return Ok(output);
        }

        [HttpPost("posting")]
        public IActionResult PostArticlesPosting(ArticlePostingRequest request)
        {
            var article = articlesPosting.PostArticle(request);

            var output = articlesApiFormatter.FormatPosting(article);

            return CreatedAtAction(nameof(GetArticleDetails), new { id = article.Id }, output);
        }
    }
}
