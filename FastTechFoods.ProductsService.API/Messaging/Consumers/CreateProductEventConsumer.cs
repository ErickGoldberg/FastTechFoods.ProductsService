﻿using FastTechFoods.ProductsService.Application.Dtos;
using FastTechFoods.ProductsService.Application.Services;
using MassTransit;
using OrderService.Contracts.Events;

namespace FastTechFoods.ProductsService.API.Messaging.Consumers;
public class CreateProductEventConsumer(IProductService productService) : IConsumer<CreateProductEvent>
{
    public async Task Consume(ConsumeContext<CreateProductEvent> context)
    {
        var message = context.Message;

        var productInputModel = MapToInputModel(message);

        var existing = await productService.GetByIdAsync(productInputModel.Id);
        if (!existing.IsSuccess)
            await productService.CreateAsync(productInputModel);
    }

    private static ProductInputModel MapToInputModel(CreateProductEvent message)
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