using FastTechFoods.ProductsService.Application.Dtos;
using FastTechFoods.ProductsService.Application.Services;
using MassTransit;
using OrderService.Contracts.Events;

namespace FastTechFoods.ProductsService.Worker
{
    public class DeleteProductEventConsumer(IProductService productService) : IConsumer<DeleteProductEvent>
    {
        public async Task Consume(ConsumeContext<DeleteProductEvent> context)
        {
            var message = context.Message;

            var existing = await productService.GetByIdAsync(message.Id);
            
            if (existing.IsSuccess)
                await productService.DeleteAsync(message.Id);
        }
    }
}
