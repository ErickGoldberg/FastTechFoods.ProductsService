using FastTechFoods.ProductsService.API.Messaging.Consumers;
using FastTechFoods.ProductsService.Application.Services;
using FastTechFoods.ProductsService.Domain.Entities;
using FastTechFoods.SDK;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FastTechFoods.ProductsService.API.Configurations
{
    public static class Extension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConnection = Environment.GetEnvironmentVariable("MONGO_DB_CONNECTION")
                                  ?? configuration.GetConnectionString("MongoDb");

            if (!string.IsNullOrWhiteSpace(mongoConnection))
            {
                services.AddMongoConnection(mongoConnection);
                services.AddMongoRepository<Product>("Products");
            }

            services.AddScoped<IProductService, ProductService>();

            return services;
        }

        public static IServiceCollection AddAutentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtKey = configuration["Jwt:Key"];

            if (!string.IsNullOrWhiteSpace(jwtKey))
            {
                services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer("Bearer", options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(jwtKey)
                            )
                        };
                    });

                services.AddAuthorization();
            }

            return services;
        }

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