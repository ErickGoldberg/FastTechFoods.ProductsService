using FastTechFoods.ProductsService.Domain.Enums;

namespace FastTechFoods.ProductsService.Application.Dtos
{
    public class ProductInputModel
    {
        public string Name { get; set; } = null!;
        public ProductTypeEnum ProductType { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public AvailabilityStatusEnum Availability { get; set; }
    }
}
