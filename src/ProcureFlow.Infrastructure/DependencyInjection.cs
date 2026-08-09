using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcureFlow.Infrastructure.Data;
using ProcureFlow.Infrastructure.Repositories;
using ProcureFlow.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;


namespace ProcureFlow.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext registration
            services.AddDbContext<ProcureFlowDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Domain Services registrations

            // Repository registrations
            services.AddScoped(typeof(IGenericRepository<>),  typeof(GenericRepository<>));

            return services;
        }
    }
}
