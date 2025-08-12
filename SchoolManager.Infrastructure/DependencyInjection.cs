using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<Context>(options => options.UseNpgsql(connectionString));
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            return services;
        }
    }
}
