using Microsoft.Extensions.DependencyInjection;

namespace Blog.Business.Categories
{
    public static class CategoriesModule
    {
        public static void AddCategoriesModule(this IServiceCollection services)
        {
            services.AddDomainModel();

            // Functionalities.

            services.AddCategoriesQuery();

            // TODO: Add new functionalities here.
        }

        private static void AddDomainModel(this IServiceCollection services)
        {
            services.AddSingleton<ICategoriesRepository>(ctx => ctx.GetService<InMemoryDataService>());
        }

        private static void AddCategoriesQuery(this IServiceCollection services)
        {
            services.AddSingleton<ICategoriesQuery>(ctx => ctx.GetService<InMemoryDataService>());
        }
    }
}
