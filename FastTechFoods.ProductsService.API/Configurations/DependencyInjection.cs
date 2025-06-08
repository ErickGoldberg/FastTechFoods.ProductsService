using FastTechFoods.ProductsService.Application.Services;
using FastTechFoods.ProductsService.Infrastructure.Persistence;
using FastTechFoods.SDK;

namespace FastTechFoods.ProductsService.API.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlConnection = configuration.GetConnectionString("SqlServer");

            services.AddSqlContext<ProductsDbContext>(sqlConnection);

            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}