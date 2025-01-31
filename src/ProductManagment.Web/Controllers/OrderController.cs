using Microsoft.AspNetCore.Mvc;
using ProductManagment.Domain.DTOs;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces.Services;
using ProductManagment.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagment.Web.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] OrderDTO orderDto)
        {
            await _orderService.AddAsync(orderDto);
            return CreatedAtAction(nameof(GetById), new { id = orderDto.Id }, orderDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAll()
        {
            return Ok(await _orderService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetById(Guid id)
        {
            return Ok(await _orderService.GetByIdAsync(id));
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateStatus(OrderDTO orderDto)
        {
            await _orderService.UpdateStatusAsync(orderDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();

            await _orderService.DeleteAsync(order);
            return NoContent();
        }
    }
}
