using FastTechFoods.ProductsService.Domain.Enums;

namespace FastTechFoods.ProductsService.Application.InputModels
{
    public class CreateOrEditProductInputModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ProductTypeEnum ProductType { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public AvailabilityStatusEnum Availability { get; set; }
    }
}