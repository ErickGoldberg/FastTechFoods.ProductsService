using System.Text.Json;
using FastTechFoods.ProductsService.Application.Dtos;
using FastTechFoods.ProductsService.Application.Services;
using FastTechFoods.SDK.MessageBus;

namespace FastTechFoods.ProductsService.Worker;

public class ProductConsumer(IServiceProvider serviceProvider) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = serviceProvider.CreateScope();
        var subscriber = scope.ServiceProvider.GetRequiredService<IEventSubscriber>();
        var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

        subscriber.Subscribe("product-events", async message =>
        {
            try
            {
                var productDto = JsonSerializer.Deserialize<ProductInputModel>(message);
                if (productDto == null)
                    return;

                var existing = await productService.GetByIdAsync(productDto.Id);
                if (existing.IsSuccess)
                    await productService.UpdateAsync(productDto.Id, productDto);
                else
                    await productService.CreateAsync(productDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
            }
        });

        return Task.CompletedTask;
    }
}