using Microsoft.Extensions.DependencyInjection;

namespace Blog.Business.Users
{
    public static class UsersModule
    {
        public static void AddUsersModule(this IServiceCollection services)
        {
            services.AddDomainModel();

            // Functionalities.

            services.AddUsersQuery();

            // TODO: Add new functionalities here.
        }

        private static void AddDomainModel(this IServiceCollection services)
        {
            services.AddSingleton<IUsersRepository, InMemoryDataService>();
        }

        private static void AddUsersQuery(this IServiceCollection services)
        {
            services.AddSingleton<IUsersQuery, InMemoryDataService>();
        }
    }
}
