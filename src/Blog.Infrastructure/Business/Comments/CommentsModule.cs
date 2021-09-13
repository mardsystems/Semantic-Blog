using Microsoft.Extensions.DependencyInjection;

namespace Blog.Business.Comments
{
    public static class CommentsModule
    {
        public static void AddCommentsModule(this IServiceCollection services)
        {
            services.AddDomainModel();

            // Functionalities.

            services.AddCommentsQuery();

            // TODO: Add new functionalities here.
        }

        private static void AddDomainModel(this IServiceCollection services)
        {
            services.AddSingleton<ICommentsRepository, InMemoryDataService>();
        }

        private static void AddCommentsQuery(this IServiceCollection services)
        {
            services.AddSingleton<ICommentsQuery, InMemoryDataService>();
        }
    }
}
