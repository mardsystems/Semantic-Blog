using Blog.Business.Articles;
using Blog.Business.Categories;
using Blog.Business.Comments;
using Blog.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog
{
    public class InMemoryDataService :
        IArticlesQuery,
        IArticlesRepository,
        ICategoriesQuery,
        ICategoriesRepository,
        ICommentsQuery,
        ICommentsRepository,
        IUsersQuery,
        IUsersRepository
    {
        private readonly List<Article> articles;

        private readonly List<Business.Categories.Category> categories;

        private readonly List<Comment> comments;

        private readonly List<User> users;

        public InMemoryDataService()
        {
            var user1 = new User(id: new UserId("u1"), name: "Usuário 1", email: "usuario1@gmail.com", imageUrl: "");
            var user2 = new User(id: new UserId("u2"), name: "Usuário 2", email: "usuario2@gmail.com", imageUrl: "");

            users = new List<User>();

            users.Add(user1);
            users.Add(user2);

            var categories1 = new Business.Categories.Category(id: new CategoryId("c1"), description: "Category C1");
            var categories2 = new Business.Categories.Category(id: new CategoryId("c2"), description: "Category C2");
            var categories3 = new Business.Categories.Category(id: new CategoryId("c3"), description: "Category C3");

            categories = new List<Business.Categories.Category>();

            categories.Add(categories1);
            categories.Add(categories2);
            categories.Add(categories3);

            articles = new List<Article>();

            articles.Add(new Article(id: new ArticleId("a"), title: "Title A", content: "Content A", categories: new[] { categories1 }));
            articles.Add(new Article(id: new ArticleId("b"), title: "Title B", content: "Content B", categories: new[] { categories3 }));
            articles.Add(new Article(id: new ArticleId("c"), title: "Title C", content: "Content C", categories: new[] { categories1, categories3 }));
            articles.Add(new Article(id: new ArticleId("d"), title: "Title D", content: "Content D", categories: new Business.Categories.Category[] { }));
            articles.Add(new Article(id: new ArticleId("e"), title: "Title E", content: "Content E", categories: new[] { categories2 }));

            comments = new List<Comment>();
        }

        public Article[] GetArticles(ArticlesQueryRequest request)
        {
            return articles
                .Where(article => request.Title == null ? true : article.Title.Contains(request.Title))
                .ToArray();
        }

        public Article GetArticle(ArticleId id)
        {
            var article = articles.FirstOrDefault(article => article.Id.Value == id.Value);

            if (article == default)
            {
                throw new ApplicationException();
            }

            return article;
        }

        public void Add(Article article)
        {
            articles.Add(article);
        }

        Business.Categories.Category[] ICategoriesQuery.GetCategories()
        {
            return categories.ToArray();
        }

        public void Add(Business.Categories.Category category)
        {
            categories.Add(category);
        }

        public Business.Categories.Category[] GetCategoriesBy(CategoryId[] ids)
        {
            return categories
                .Where(category => ids.Select(id => id.Value).Contains(category.Id.Value))
                .ToArray();
        }

        public Comment[] GetComments()
        {
            return comments.ToArray();
        }

        public void Add(Comment comment)
        {
            comments.Add(comment);
        }

        public User[] GetUsers()
        {
            return users.ToArray();
        }

        public void Add(User user)
        {
            users.Add(user);
        }
    }
}
