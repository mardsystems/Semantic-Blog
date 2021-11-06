using System;

namespace Blog.Data
{
    public class Comment
    {
        public int ArticleId { get; set; }

        public int CommentId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
