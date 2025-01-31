using ProductManagment.Domain.Interfaces;
using ProductManagment.Infrastructure.Context;
using ProductManagment.Infrastructure.Repositories;
using ProductManagment.Infrastructure;
using ProductManagment.Domain.Interfaces.Services;
using ProductManagment.Application.Services;
using ProductManagment.Domain.Core.Interface;
using ProductManagment.Application.Validations;
using ProductManagment.Domain.Entities;
using FluentValidation;
using ProductManagment.Domain.Interfaces.Repositories;
using ProductManagment.Domain.DTOs;
using ProductManagment.CrossCutting;

namespace ProductManagment.Web.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(AppProfile));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IStockRepository, StockRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IStockService, StockService>();

            services.AddScoped<IValidator<ProductDTO>, ProductValidator>();
            services.AddScoped<IValidator<ClientDTO>, ClientValidator>();
            services.AddScoped<IValidator<OrderDTO>, OrderValidator>();
            services.AddScoped<IValidator<StockDTO>, StockValidator>();

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IDbFactory, DbFactory>();

            return services;

        }
    }
}
