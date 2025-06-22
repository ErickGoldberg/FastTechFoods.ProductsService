using FastTechFoods.ProductsService.Application.Dtos;
using FastTechFoods.ProductsService.Domain.Entities;
using FastTechFoods.ProductsService.Domain.Enums;
using FastTechFoods.SDK.Abstraction;
using FastTechFoods.SDK.Persistence.Repository;

namespace FastTechFoods.ProductsService.Application.Services
{
    public class ProductService(IMongoRepository<Product> productRepository) : IProductService
    {
        public async Task<Result<List<ProductDto>>> GetAllAsync()
        {
            var products = await productRepository.GetAllAsync();
            var result = products.Select(p => new ProductDto
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
            var product = await productRepository.GetByIdAsync(id.ToString());
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

        public async Task<Result<List<ProductDto>>> GetListProductsAsync(List<Guid> ids)
        {
            if (ids == null || !ids.Any())
                return Result<List<ProductDto>>.Failure("Lista de IDs não pode ser nula ou vazia.");

            var products = await productRepository.GetListAsync(ids.Select(x => x.ToString()).ToList());

            var mappedProducts = products
                .Select(product => new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    ProductType = product.ProductType,
                    Description = product.Description,
                    Price = product.Price,
                    IsAvailable = product.Availability == AvailabilityStatusEnum.Available

                })
                .ToList();

            return Result<List<ProductDto>>.Success(mappedProducts);
        }

        public async Task<Result<List<ProductDto>>> GetByTypeAsync(ProductTypeEnum productType)
        {
            var products = await productRepository.FindAsync(p => p.ProductType == productType);
            var result = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                ProductType = p.ProductType,
                Price = p.Price
            }).ToList();

            return Result<List<ProductDto>>.Success(result);
        }

        public async Task<Result> CreateAsync(ProductInputModel dto)
        {
            var product = new Product(
                name: dto.Name,
                productType: dto.ProductType,
                price: dto.Price,
                description: dto.Description,
                availability: dto.Availability
            );

            await productRepository.AddAsync(product);

            return Result.Created();
        }

        public async Task<Result> UpdateAsync(Guid id, ProductInputModel dto)
        {
            var existing = await productRepository.GetByIdAsync(id.ToString());
            if (existing == null)
                return Result<string>.Failure("Produto não encontrado.");

            existing.Update(dto.Name, dto.ProductType, dto.Price, dto.Description);
            existing.ChangeAvailability(dto.Availability);

            await productRepository.UpdateAsync(id.ToString(), existing);

            return Result.Success();
        }


        public async Task<Result> DeleteAsync(Guid id)
        {
            var existing = await productRepository.GetByIdAsync(id.ToString());
            if (existing == null)
                return Result<string>.Failure("Produto não encontrado.");

            await productRepository.DeleteAsync(id.ToString());

            return Result.Success();
        }
    }
}