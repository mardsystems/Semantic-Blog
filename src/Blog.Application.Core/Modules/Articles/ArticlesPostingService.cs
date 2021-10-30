using Blog.Modules.Categories;
using Blog.Modules.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Modules.Articles
{
    public class ArticlesPostingService : IArticlesPosting
    {
        private readonly ArticlesPostingContext context;

        private readonly IArticlesRepository articlesRepository;

        private readonly ICategoriesRepository categoriesRepository;

        private readonly ICommentsRepository commentsRepository;

        public ArticlesPostingService(
            ArticlesPostingContext context,
            IArticlesRepository articlesRepository,
            ICategoriesRepository categoriesRepository,
            ICommentsRepository commentsRepository)
        {
            this.context = context;

            this.articlesRepository = articlesRepository;

            this.categoriesRepository = categoriesRepository;

            this.commentsRepository = commentsRepository;
        }

        public Article PostArticle(ArticlePostingRequest request)
        {
            var id = Guid.NewGuid().ToString();

            var articleId = new ArticleId(id);

            var categories = categoriesRepository.GetCategoriesBy(request.CategoriesIds);

            var article =
                new Article(
                    id: articleId,
                    title: request.Title,
                    content: request.Content,
                    categories: categories);

            articlesRepository.Add(article);

            return article;
        }
    }
}
