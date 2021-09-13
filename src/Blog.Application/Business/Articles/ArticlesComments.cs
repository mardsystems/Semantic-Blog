using Blog.Business.Comments;
using Blog.Business.Users;

namespace Blog.Business.Articles
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
