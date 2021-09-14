using Microsoft.Extensions.DependencyInjection;

namespace Blog.Business.Articles
{
    public static class ArticlesModule
    {
        public static void AddArticlesModule(this IServiceCollection services)
        {
            services.AddDomainModel();

            // Functionalities.

            services.AddArticlesQuery();

            services.AddArticlesComments();

            services.AddArticlesPosting();

            // TODO: Add new functionalities here.
        }

        private static void AddDomainModel(this IServiceCollection services)
        {
            services.AddSingleton<IArticlesRepository>(ctx => ctx.GetService<InMemoryDataService>());
        }

        private static void AddArticlesQuery(this IServiceCollection services)
        {
            services.AddSingleton<IArticlesQuery>(ctx => ctx.GetService<InMemoryDataService>());
        }

        private static void AddArticlesComments(this IServiceCollection services)
        {
            services.AddSingleton<IArticlesComments, ArticlesCommentsService>();

            services.AddTransient<ArticlesCommentsContext>();
        }

        private static void AddArticlesPosting(this IServiceCollection services)
        {
            services.AddSingleton<IArticlesPosting, ArticlesPostingService>();

            services.AddTransient<ArticlesPostingContext>();
        }
    }
}
