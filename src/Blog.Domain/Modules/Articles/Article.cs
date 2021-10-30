using Blog.Modules.Comments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Modules.Articles
{
    public class Article
    {
        public ArticleId Id { get; private set; }

        public string Title { get; private set; }

        public string Content { get; private set; }

        private readonly ICollection<Category> categories;

        public virtual IEnumerable<Category> Categories { get => categories; }

        private readonly ICollection<Comment> comments;

        public virtual IEnumerable<Comment> Comments { get => comments; }

        public Article(
            ArticleId id,
            string title,
            string content,
            Categories.Category[] categories)
        {
            Id = id;

            Title = title;

            Content = content;

            this.categories = new HashSet<Category>();

            foreach (var category in categories)
            {
                category.IncrementTotalArticles();

                this.categories.Add((Category)category);
            }

            comments = new HashSet<Comment>();
        }

        private Article()
        {
            categories = new HashSet<Category>();

            comments = new HashSet<Comment>();
        }
    }

    public class ArticleId : ValueObject
    {
        public string Value { get; private set; }

        public ArticleId(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public interface IArticlesRepository
    {
        void Add(Article article);
    }
}
