using FastTechFoods.ProductsService.Application.Services;
using FastTechFoods.ProductsService.Domain.Entities;
using FastTechFoods.ProductsService.Worker;
using FastTechFoods.SDK;
using FastTechFoods.SDK.MessageBus;

var builder = Host.CreateApplicationBuilder(args);
var config = builder.Configuration;

builder.Services.AddMongoConnection(config.GetConnectionString("MongoDb"));
builder.Services.AddMongoRepository<Product>("Products");

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<IEventSubscriber, RabbitMqEventSubscriber>();

builder.Services.AddRabbitMqConnectionAsync("rabbitmq", "guest", "guest");

builder.Services.AddHostedService<ProductConsumer>();

var host = builder.Build();
host.Run();