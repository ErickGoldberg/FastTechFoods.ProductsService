using FastTechFoods.ProductsService.Application.Services;
using MassTransit;
using OrderService.Contracts.Events;

namespace FastTechFoods.ProductsService.API.Messaging.Consumers
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
