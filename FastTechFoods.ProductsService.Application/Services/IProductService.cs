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
        Task<Result<List<ProductDto>>> GetListProductsAsync(List<Guid> ids);
        Task<Result> CreateAsync(ProductInputModel dto);
        Task<Result> UpdateAsync(Guid id, ProductInputModel dto);
        Task<Result> DeleteAsync(Guid id);
    }
}