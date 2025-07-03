using MassTransit;
using OrderService.Contracts.Events;

namespace FastTechFoods.ProductsService.Worker
{
    public static class Extensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            var envHostRabbitMqServer = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Message<CreateProductEvent>(x =>
                    {
                        x.SetEntityName("create-product-event");
                    });

                    cfg.Host(envHostRabbitMqServer);
                });
            });

            return services;
        }
    }
}
