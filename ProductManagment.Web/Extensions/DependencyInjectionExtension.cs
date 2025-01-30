using ProductManagment.Domain.Base;
using ProductManagment.Domain.Interfaces;
using ProductManagment.Infrastructure.Context;
using ProductManagment.Infrastructure.Repositories;
using ProductManagment.Infrastructure;
using ProductManagment.Domain.Interfaces.Services;
using ProductManagment.Application.Services;

namespace ProductManagment.Web.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IStockService, StockService>();


            // Registro do repositório genérico
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IDbFactory, DbFactory>();

            return services;

        }
    }
}
