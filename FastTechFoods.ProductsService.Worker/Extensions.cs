using MassTransit;
using OrderService.Contracts.Events;

namespace FastTechFoods.ProductsService.Worker
{
    public static class Extensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            var envHostRabbitMqServer = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "rabbitmq://localhost:31001";

            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateProductEventConsumer>();
                x.AddConsumer<DeleteProductEventConsumer>();
                x.AddConsumer<UpdateProductEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("create-product-event", e =>
                    {
                        e.ConfigureConsumer<CreateProductEventConsumer>(context);
                    }); 
                    
                    cfg.ReceiveEndpoint("delete-product-event", e =>
                    {
                        e.ConfigureConsumer<DeleteProductEventConsumer>(context);
                    }); 
                    
                    cfg.ReceiveEndpoint("update-product-event", e =>
                    {
                        e.ConfigureConsumer<UpdateProductEventConsumer>(context);
                    });

                    cfg.Host(envHostRabbitMqServer);
                });
            });

            return services;
        }
    }
}
