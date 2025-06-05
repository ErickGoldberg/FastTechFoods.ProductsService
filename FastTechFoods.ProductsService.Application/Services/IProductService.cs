using FastTechFoods.ProductsService.Application.Dtos;
using FastTechFoods.ProductsService.Application.InputModels;
using FastTechFoods.SDK.Abstraction;

namespace FastTechFoods.ProductsService.Application.Services
{
    public interface IProductService
    {
        Task<Result<List<ProductDto>>> GetAllAsync();
        Task<Result<ProductDto>> GetByIdAsync(Guid id);
        Task<Result<List<ProductDto>>> GetByTypeAsync(string type);
        Task<Result> CreateAsync(CreateOrEditProductInputModel model);
        Task<Result> UpdateAsync(CreateOrEditProductInputModel model);
        Task<Result> DeleteAsync(Guid id);
    }
}