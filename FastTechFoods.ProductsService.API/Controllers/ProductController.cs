using FastTechFoods.ProductsService.Application.Dtos;
using FastTechFoods.ProductsService.Application.InputModels;
using FastTechFoods.ProductsService.Application.Services;
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

            if (result.Data == null || !result.Data.Any())
                return NotFound();

            return Ok(result.Data);
        }

        /// <summary>
        /// Retorna produtos filtrados por tipo de refeição (ex: lanche, sobremesa, bebida).
        /// </summary>
        [HttpGet("filter-by-type")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(List<ProductDto>))]
        public async Task<IActionResult> GetByType([FromQuery] string type)
        {
            var result = await productService.GetByTypeAsync(type);

            if (result.Data == null || !result.Data.Any())
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

            if (result.Data == null)
                return NotFound();

            return Ok(result.Data);
        }

        /// <summary>
        /// Adiciona um novo produto ao cardápio.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert(CreateOrEditProductInputModel model)
        {
            var result = await productService.CreateAsync(model);

            if (result.IsSuccess)
                return Created();

            return BadRequest(result.Message);
        }

        /// <summary>
        /// Atualiza um produto existente no cardápio.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(CreateOrEditProductInputModel model)
        {
            var result = await productService.UpdateAsync(model);

            if (result.IsSuccess)
                return NoContent();

            return BadRequest(result.Message);
        }

        /// <summary>
        /// Remove um produto do cardápio pelo ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productService.DeleteAsync(id);

            if (result.IsSuccess)
                return NoContent();

            return BadRequest(result.Message);
        }
    }
}