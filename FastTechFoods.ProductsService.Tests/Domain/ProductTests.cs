using FastTechFoods.ProductsService.Domain.Entities;
using FastTechFoods.ProductsService.Domain.Enums;
using FluentAssertions;

namespace FastTechFoods.ProductsService.Tests.Domain
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void Constructor_ValidParams_ShouldCreateProduct()
        {
            // Arrange
            var name = "X-Burger";
            var type = ProductTypeEnum.Meal;
            var price = 25.50m;
            var description = "Hamburguer artesanal";
            var availability = AvailabilityStatusEnum.Available;

            // Act
            var product = new Product(name, type, price, description, availability);

            // Assert
            product.Name.Should().Be(name);
            product.ProductType.Should().Be(type);
            product.Price.Should().Be(price);
            product.Description.Should().Be(description);
            product.Availability.Should().Be(availability);
        }

        [Test]
        public void Update_ValidParams_ShouldUpdateFields()
        {
            // Arrange
            var product = new Product("Old", ProductTypeEnum.Dessert, 10m, "Old Desc", AvailabilityStatusEnum.Available);
            var newName = "New Burger";
            var newType = ProductTypeEnum.Drink;
            var newPrice = 12.99m;
            var newDesc = "Updated Description";

            // Act
            product.Update(newName, newType, newPrice, newDesc);

            // Assert
            product.Name.Should().Be(newName);
            product.ProductType.Should().Be(newType);
            product.Price.Should().Be(newPrice);
            product.Description.Should().Be(newDesc);
            product.UpdatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }

        [Test]
        public void Update_InvalidParams_ShouldNotUpdateFields()
        {
            // Arrange
            var original = new Product("Original", ProductTypeEnum.Drink, 10m, "Desc", AvailabilityStatusEnum.Available);
            var updatedAtBefore = original.UpdatedAt;

            // Act
            original.Update(null, ProductTypeEnum.None, decimal.MinValue, null);

            // Assert
            original.Name.Should().Be("Original");
            original.ProductType.Should().Be(ProductTypeEnum.Drink);
            original.Price.Should().Be(10m);
            original.Description.Should().Be("Desc");
            original.UpdatedAt.Should().NotBe(updatedAtBefore); 
        }

        [Test]
        public void ChangeAvailability_ShouldUpdateAvailabilityAndTimestamp()
        {
            // Arrange
            var product = new Product("Item", ProductTypeEnum.Drink, 10m, "Desc", AvailabilityStatusEnum.Available);
            var newStatus = AvailabilityStatusEnum.OutOfStock;

            // Act
            product.ChangeAvailability(newStatus);

            // Assert
            product.Availability.Should().Be(newStatus);
            product.UpdatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }
    }
}
