using Application.Common.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class RegisterServices
    {
        public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Set DTO configuration
            services.AddAutoMapper(typeof(Mapper));
            //Set MediatR configuration
            services.AddMediatR(_=>_.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }
    }
}
