using FastTechFoods.ProductsService.Application.Services;
using FastTechFoods.ProductsService.Domain.Entities;
using FastTechFoods.SDK;
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
                services.AddAuthentication("Bearer")
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
    }
}