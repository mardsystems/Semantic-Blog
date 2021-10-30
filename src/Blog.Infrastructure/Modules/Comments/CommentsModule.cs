using Microsoft.Extensions.DependencyInjection;

namespace Blog.Modules.Comments
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
            services.AddSingleton<ICommentsRepository>(ctx => ctx.GetService<InMemoryDataService>());
        }

        private static void AddCommentsQuery(this IServiceCollection services)
        {
            services.AddSingleton<ICommentsQuery>(ctx => ctx.GetService<InMemoryDataService>());
        }
    }
}
