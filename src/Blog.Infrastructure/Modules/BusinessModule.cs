using Blog.Modules.Articles;
using Blog.Modules.Categories;
using Blog.Modules.Comments;
using Blog.Modules.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Modules
{
    public static class ModulesModule
    {
        public static void AddModulesModule(this IServiceCollection services)
        {
            services.AddArticlesModule();
            
            services.AddCategoriesModule();
            
            services.AddCommentsModule();
            
            services.AddUsersModule();

            // TODO: Add new business modules here.
        }
    }
}
