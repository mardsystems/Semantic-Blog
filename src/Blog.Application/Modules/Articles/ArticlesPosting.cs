using Blog.Modules.Categories;

namespace Blog.Modules.Articles
{
    public interface IArticlesPosting
    {
        Article PostArticle(ArticlePostingRequest request);
    }

    public class ArticlePostingRequest
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public CategoryId[] CategoriesIds { get; set; }
    }
}
