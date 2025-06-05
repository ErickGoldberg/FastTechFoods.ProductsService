using FastTechFoods.SDK.Domain;

namespace FastTechFoods.ProductsService.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; } 
        public string Type { get; private set; }
        public decimal Price { get; private set; }

        // Construtor para o E.F
        protected Product() { }

        public Product(string name, string type, decimal price)
        {
            Name = name;
            Type = type;
            Price = price;
        }

        public void Update(string name, string type, decimal price)
        {
            Name = name;
            Type = type;
            Price = price;
            UpdatedAt = DateTime.Now;
        }
    }
}