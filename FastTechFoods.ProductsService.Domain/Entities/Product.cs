using FastTechFoods.ProductsService.Domain.Enums;
using FastTechFoods.SDK.Domain;

namespace FastTechFoods.ProductsService.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public ProductTypeEnum ProductType { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public AvailabilityStatusEnum Availability { get; private set; }

        protected Product() { }

        public Product(Guid id, string name, ProductTypeEnum productType, decimal price, string description, AvailabilityStatusEnum availability)
        {
            Id = id;
            Name = name;
            ProductType = productType;
            Price = price;
            Description = description;
            Availability = availability;
        }

        public void Update(string name, ProductTypeEnum productType, decimal price, string description)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name;

            if (!string.IsNullOrWhiteSpace(name))
                Description = description;

            if (price != decimal.MinValue)
                Price = price;

            if (productType != ProductTypeEnum.None)
                ProductType = productType;

            UpdatedAt = DateTime.Now;
        }

        public void ChangeAvailability(AvailabilityStatusEnum statusEnum)
        {
            Availability = statusEnum;
            UpdatedAt = DateTime.Now;
        }
    }
}