namespace Blog.Modules.Articles
{
    public interface IArticlesApiFormatter
    {
        public object Format(Article[] articles);
        
        public object Format(ArticlesQueryRequest request);
        
        object FormatDetails(Article article);
        
        object FormatPosting(ArticlePostingRequest request);
        
        object FormatPosting(Article article);
    }
}
