using FastTechFoods.ProductsService.Application.Dtos;
using FastTechFoods.ProductsService.Domain.Enums;
using FastTechFoods.SDK.Abstraction;

namespace FastTechFoods.ProductsService.Application.Services
{
    public interface IProductService
    {
        Task<Result<List<ProductDto>>> GetAllAsync();
        Task<Result<ProductDto>> GetByIdAsync(Guid id);
        Task<Result<List<ProductDto>>> GetByTypeAsync(ProductTypeEnum productType);
    }
}