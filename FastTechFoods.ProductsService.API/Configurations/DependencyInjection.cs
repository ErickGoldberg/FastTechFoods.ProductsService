using FastTechFoods.ProductsService.Application.Services;
using FastTechFoods.ProductsService.Infrastructure.Persistence;
using FastTechFoods.SDK;

namespace FastTechFoods.ProductsService.API.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlConnection = configuration.GetConnectionString("SqlServer")!;
            var mongoConnection = configuration.GetConnectionString("Mongo")!;

            services
                .AddSqlContext<ProductsDbContext>(sqlConnection)
                .AddMongoConnection(mongoConnection);

            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}