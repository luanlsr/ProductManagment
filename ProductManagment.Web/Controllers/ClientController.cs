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
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ClientDTO clientDto)
        {
            await _clientService.AddAsync(clientDto);
            return CreatedAtAction(nameof(GetById), new { id = clientDto.Id }, clientDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAll()
        {
            return Ok(await _clientService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(Guid id)
        {
            return Ok(await _clientService.GetByIdAsync(id));
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] ClientDTO clientDto)
        {
            if (id != clientDto.Id) return BadRequest("ID mismatch");

            await _clientService.UpdateAsync(clientDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var client = await _clientService.GetByIdAsync(id);
            if (client == null) return NotFound();

            await _clientService.DeleteAsync(client);
            return NoContent();
        }
    }
}
