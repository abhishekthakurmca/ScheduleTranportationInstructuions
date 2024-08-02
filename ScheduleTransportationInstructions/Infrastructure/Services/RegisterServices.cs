using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public static class RegisterServices
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IScheduleTransportRepository, ScheduleTransportRepository>();
            services.AddScoped<IInstructionRepository, InstructionRepository>();
            services.AddDbContext<ApplicationDbContext>();

        }
    }
}
