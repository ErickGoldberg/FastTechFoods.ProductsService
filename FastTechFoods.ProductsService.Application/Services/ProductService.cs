using FastTechFoods.ProductsService.Application.Dtos;
using FastTechFoods.ProductsService.Domain.Entities;
using FastTechFoods.ProductsService.Domain.Enums;
using FastTechFoods.SDK.Abstraction;
using FastTechFoods.SDK.Persistence.Repository;

namespace FastTechFoods.ProductsService.Application.Services
{
    public class ProductService(IUnitOfWork unitOfWork) : IProductService
    {
        public async Task<Result<List<ProductDto>>> GetAllAsync()
        {
            var products = await unitOfWork.Repository<Product>().GetAllAsync();

            var result = products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ProductType = p.ProductType,
                    Price = p.Price
                }).ToList();

            return Result<List<ProductDto>>.Success(result);
        }

        public async Task<Result<ProductDto>> GetByIdAsync(Guid id)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
            if (product == null)
                return Result<ProductDto>.Failure("Produto não encontrado.");

            return Result<ProductDto>.Success(new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                ProductType = product.ProductType,
                Price = product.Price
            });
        }

        public async Task<Result<List<ProductDto>>> GetByTypeAsync(ProductTypeEnum productType)
        {
            var products = await unitOfWork.Repository<Product>().FindAsync(p => p.ProductType == productType);

            var result = products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ProductType = p.ProductType,
                    Price = p.Price
                }).ToList();

            return Result<List<ProductDto>>.Success(result);
        }
    }
}