using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.Services;
using ProductManagment.Domain.DTOs;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagment.Web.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] StockDTO stockDto)
        {
            await _stockService.AddAsync(stockDto);
            return CreatedAtAction(nameof(GetById), new { id = stockDto.Id }, stockDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockDTO>>> GetAll()
        {
            return Ok(await _stockService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockDTO>> GetById(Guid id)
        {
            return Ok(await _stockService.GetByIdAsync(id));
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var count = await _stockService.GetCountAsync();
            return Ok(new { TotalStocks = count });
        }


        [HttpPut("{productId}/update")]
        public async Task<ActionResult> UpdateStock([FromBody] StockDTO stockDto)
        {
            await _stockService.UpdateAsync(stockDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var stock = await _stockService.GetByIdAsync(id);
            if (stock == null) return NotFound();

            await _stockService.DeleteAsync(stock);
            return NoContent();
        }
    }
}
