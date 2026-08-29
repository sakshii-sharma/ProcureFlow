using FluentValidation;
using ProcureFlow.Application.Features.Categories.Interfaces;
using ProcureFlow.Application.Features.Categories.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcureFlow.Application.Features.Products.Interfaces;
using ProcureFlow.Application.Features.Products.Services;
using ProcureFlow.Application.Features.Warehouses.Interfaces;
using ProcureFlow.Application.Features.Warehouses.Services;
using ProcureFlow.Application.Features.Suppliers.Interfaces;
using ProcureFlow.Application.Features.Suppliers.Services;

namespace ProcureFlow.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Application Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<ISupplierService, SupplierService>();

            // Validators
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
