using Blog.Business;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog
{
    public static class InfraModule
    {
        public static void AddInfraModule(this IServiceCollection services)
        {
            services.AddBusinessModule();
        }
    }
}
