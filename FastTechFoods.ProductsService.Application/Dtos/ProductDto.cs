using FastTechFoods.ProductsService.Domain.Enums;

namespace FastTechFoods.ProductsService.Application.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Availability { get; set; }
        public ProductTypeEnum ProductType { get; set; }
        public decimal Price { get; set; }
    }
}