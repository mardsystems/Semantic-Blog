using Blog.Business.Categories;

namespace Blog.Business.Articles
{
    public interface IArticlesPosting
    {
        ArticleId PostArticle(ArticlePostingRequest request);
    }

    public class ArticlePostingRequest
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public CategoryId[] CategoriesIds { get; set; }
    }
}
