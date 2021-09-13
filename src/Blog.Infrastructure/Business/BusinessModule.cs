using Blog.Business.Articles;
using Blog.Business.Categories;
using Blog.Business.Comments;
using Blog.Business.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business
{
    public static class BusinessModule
    {
        public static void AddBusinessModule(this IServiceCollection services)
        {
            services.AddArticlesModule();
            
            services.AddCategoriesModule();
            
            services.AddCommentsModule();
            
            services.AddUsersModule();

            // TODO: Add new business modules here.
        }
    }
}
