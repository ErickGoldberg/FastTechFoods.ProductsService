using FastTechFoods.ProductsService.Application.Services;
using FastTechFoods.ProductsService.Domain.Entities;
using FastTechFoods.ProductsService.Worker;
using FastTechFoods.SDK;

var builder = Host.CreateApplicationBuilder(args);
var config = builder.Configuration;

var mongoConnection = Environment.GetEnvironmentVariable("MONGO_DB_CONNECTION")
                      ?? config.GetConnectionString("MongoDb");

if (!string.IsNullOrWhiteSpace(mongoConnection))
{
    builder.Services.AddMongoConnection(mongoConnection);
    builder.Services.AddMongoRepository<Product>("Products");
}

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddMessaging();

var host = builder.Build();
host.Run();