using Microsoft.EntityFrameworkCore;
using ProductManagment.Infrastructure.Context;

namespace ProductManagment.Web.Extensions
{
    public static class EntityFrameworkExtension
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, opt => opt.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds));

                options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);

                if (environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                }
            });

            return services;
        }
    }
}
