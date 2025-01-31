using Microsoft.AspNetCore.Mvc;
using ProductManagment.Domain.DTOs;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagment.Web.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductDTO productDto)
        {
            await _productService.AddAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(Guid id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.Id) return BadRequest("ID mismatch");

            await _productService.UpdateAsync(productDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            await _productService.DeleteAsync(product);
            return NoContent();
        }
    }
}
