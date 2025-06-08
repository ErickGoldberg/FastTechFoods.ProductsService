using FastTechFoods.ProductsService.Application.Dtos;
using FastTechFoods.ProductsService.Application.Services;
using FastTechFoods.ProductsService.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FastTechFoods.ProductsService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        /// <summary>
        /// Retorna todos os produtos disponíveis.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(List<ProductDto>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await productService.GetAllAsync();

            if (result?.Data == null || !result.Data.Any())
                return NotFound();

            return Ok(result.Data);
        }

        /// <summary>
        /// Retorna os detalhes de um produto específico pelo ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(ProductDto))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await productService.GetByIdAsync(id);

            if (result?.Data == null)
                return NotFound();

            return Ok(result.Data);
        }

        /// <summary>
        /// Retorna produtos filtrados por tipo de refeição (ex: Drink, dessert, Snack, Meal).
        /// </summary>
        [HttpGet("filter-by-type")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(List<ProductDto>))]
        public async Task<IActionResult> GetByType([FromQuery] ProductTypeEnum type)
        {
            var result = await productService.GetByTypeAsync(type);

            if (result?.Data == null || !result.Data.Any())
                return NotFound();

            return Ok(result.Data);
        }

    }
}