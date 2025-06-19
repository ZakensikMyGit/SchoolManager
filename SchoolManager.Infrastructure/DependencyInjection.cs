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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IChildRepository, ChildRepository>();
           services.AddTransient<IScheduleRepository, ScheduleRepository>();
            return services;
        }
    }
}
