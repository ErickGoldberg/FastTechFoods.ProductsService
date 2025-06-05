namespace FastTechFoods.ProductsService.Application.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public decimal Price { get; set; }
    }
}