using FastTechFoods.ProductsService.Application.Services;
using FastTechFoods.ProductsService.Domain.Entities;
using FastTechFoods.SDK;
using FastTechFoods.SDK.MessageBus;

namespace FastTechFoods.ProductsService.API.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConnection = configuration.GetConnectionString("MongoDb");
            services.AddMongoConnection(mongoConnection);
            services.AddMongoRepository<Product>("Products");

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IEventSubscriber, RabbitMqEventSubscriber>();

            return services;
        }
    }
}