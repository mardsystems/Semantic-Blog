using Blog.Modules.Comments;
using Blog.Modules.Users;

namespace Blog.Modules.Articles
{
    public interface IArticlesComments
    {
        CommentId CommentArticle(ArticleCommentRequest request);
    }

    public class ArticleCommentRequest
    {
        public ArticleId ArticleId { get; set; }

        public string CommentText { get; set; }

        public UserId UserId { get; set; }
    }
}
