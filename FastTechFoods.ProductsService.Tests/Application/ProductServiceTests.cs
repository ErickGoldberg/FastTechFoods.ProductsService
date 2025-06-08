using FastTechFoods.ProductsService.Application.Services;
using FastTechFoods.ProductsService.Domain.Entities;
using FastTechFoods.ProductsService.Domain.Enums;
using FastTechFoods.SDK.Persistence.Repository;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace FastTechFoods.ProductsService.Tests.Application
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock = null!;
        private Mock<IRepository<Product>> _productRepoMock = null!;
        private ProductService _service = null!;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _productRepoMock = new Mock<IRepository<Product>>();

            _unitOfWorkMock
                .Setup(u => u.Repository<Product>())
                .Returns(_productRepoMock.Object);

            _service = new ProductService(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListOfProductDtos()
        {
            // Arrange
            var products = new List<Product>
            {
                new("Item A", ProductTypeEnum.Meal, 10m, "desc", AvailabilityStatusEnum.Available),
                new("Item B", ProductTypeEnum.Drink, 20m, "desc", AvailabilityStatusEnum.Available)
            };

            _productRepoMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(products);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().HaveCount(2);
            result.Data[0].Name.Should().Be("Item A");
        }

        [Test]
        public async Task GetByIdAsync_NonExistingProduct_ShouldReturnFailure()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _productRepoMock
                .Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            // Act
            var result = await _service.GetByIdAsync(productId);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }

        [Test]
        public async Task GetByTypeAsync_ShouldReturnMatchingProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new("Item A", ProductTypeEnum.Meal, 12m, "desc", AvailabilityStatusEnum.Available),
                new("Item B", ProductTypeEnum.Meal, 14m, "desc", AvailabilityStatusEnum.Available)
            };

            _productRepoMock
                .Setup(r => r.FindAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(products);

            // Act
            var result = await _service.GetByTypeAsync(ProductTypeEnum.Meal);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().HaveCount(2);
            result.Data[0].ProductType.Should().Be(ProductTypeEnum.Meal);
        }
    }
}
