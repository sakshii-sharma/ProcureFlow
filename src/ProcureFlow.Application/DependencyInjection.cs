using FluentValidation;
using ProcureFlow.Application.Features.Categories.Interfaces;
using ProcureFlow.Application.Features.Categories.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureFlow.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Application Services
            services.AddScoped<ICategoryService, CategoryService>();

            // Validators
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
