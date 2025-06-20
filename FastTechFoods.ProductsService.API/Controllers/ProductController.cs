﻿using FastTechFoods.ProductsService.Application.Dtos;
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

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProductInputModel dto)
        {
            var result = await productService.CreateAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Created();
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductInputModel dto)
        {
            var result = await productService.UpdateAsync(id, dto);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return NoContent();
        }

        /// <summary>
        /// Remove um produto.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productService.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return NoContent();
        }
    }
}