using Blog.Business.Articles;
using System;
using System.Collections.Generic;

namespace Blog.Business.Comments
{
    public class Comment
    {
        public ArticleId ArticleId { get; set; }

        public CommentId CommentId { get; private set; }

        public string Text { get; private set; }

        public DateTime CreatedAt { get; set; }

        public Comment(
            ArticleId articleId,
            CommentId commentid,
            string text,
            DateTime createdAt)
        {
            ArticleId = articleId;

            CommentId = commentid;

            Text = text;

            CreatedAt = createdAt;
        }
    }

    public class CommentId : ValueObject
    {
        public string Value { get; private set; }

        public CommentId(string value)
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

    public interface ICommentsRepository
    {
        void Add(Comment comment);
    }
}
