using FastTechFoods.SDK.Middleware;
using FastTechFoods.SDK.Persistence.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace FastTechFoods.SDK
{
    public static class SdkModule
    {
        public static IServiceCollection AddMongoConnection(this IServiceCollection services, string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            services.AddSingleton<IMongoClient>(mongoClient);

            var mongoUrl = new MongoUrl(connectionString);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            services.AddSingleton(database);

            return services;
        }

        public static IServiceCollection AddSqlContext<TContext>(this IServiceCollection services, string connectionString)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();

            return services;
        }

        public static IServiceCollection AddRabbitMqConnectionAsync(
            this IServiceCollection services,
            string hostName,
            string userName,
            string password,
            int port = 5672
        )
        {
            var factory = new ConnectionFactory
            {
                HostName = hostName,
                Port = port,
                UserName = userName,
                Password = password
            };

            IConnection? connection = null;
            int attempts = 5;

            for (int i = 1; i <= attempts; i++)
            {
                try
                {
                    connection = factory.CreateConnection();
                    Console.WriteLine($"[RabbitMQ] Conectado com sucesso na tentativa {i}.");
                    break;
                }
                catch (BrokerUnreachableException ex)
                {
                    Console.WriteLine($"[RabbitMQ] Tentativa {i}/{attempts} falhou: {ex.Message}. Retentando em 5 segundos...");
                    Thread.Sleep(5000);
                }
            }

            if (connection is null)
                throw new Exception("[RabbitMQ] Falha ao conectar após múltiplas tentativas.");

            services.AddSingleton(connection);
            services.AddSingleton<IModel>(sp => connection.CreateModel());

            return services;
        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName)
            where T : class
        {
            services.AddScoped<IMongoRepository<T>>(sp =>
            {
                var db = sp.GetRequiredService<IMongoDatabase>();
                return new MongoRepository<T>(db, collectionName);
            });

            return services;
        }

        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        }
    }
}