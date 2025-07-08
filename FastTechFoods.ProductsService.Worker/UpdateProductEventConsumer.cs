using FastTechFoods.ProductsService.Application.Dtos;
using FastTechFoods.ProductsService.Application.Services;
using MassTransit;
using OrderService.Contracts.Events;

namespace FastTechFoods.ProductsService.Worker
{
    public class UpdateProductEventConsumer(IProductService productService) : IConsumer<UpdateProductEvent>
    {
        public async Task Consume(ConsumeContext<UpdateProductEvent> context)
        {
            var message = context.Message;

            var productInputModel = MapToInputModel(message);

            var existing = await productService.GetByIdAsync(productInputModel.Id);
            
            if (existing.IsSuccess)
                await productService.UpdateAsync(productInputModel.Id, productInputModel);
        }

        private static ProductInputModel MapToInputModel(UpdateProductEvent message)
        {
            return new ProductInputModel
            {
                Id = message.Id,
                Name = message.Name,
                Description = message.Description,
                Price = message.Price,
                ProductType = (Domain.Enums.ProductTypeEnum)(int)message.ProductType,
                Availability = (Domain.Enums.AvailabilityStatusEnum)(int)message.Availability,
            };
        }
    }
}
