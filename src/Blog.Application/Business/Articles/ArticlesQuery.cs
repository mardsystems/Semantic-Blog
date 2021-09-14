namespace Blog.Business.Articles
{
    public interface IArticlesQuery
    {
        Article[] GetArticles(ArticlesQueryRequest request);

        Article GetArticle(ArticleId id);
    }

    public class ArticlesQueryRequest
    {
        public string Title { get; set; }
    }
}
