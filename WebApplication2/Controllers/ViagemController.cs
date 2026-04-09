using Microsoft.AspNetCore.Mvc;
using WebApplication2.Application;
using WebApplication2.Domain.Entities;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("v1/[Controller]")]
    public class ViagemController : ControllerBase
    {
        private readonly ViagemService _service;

        public ViagemController(ViagemService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] Viagem viagem,
            CancellationToken token = default)
        {
            var viagemCriada = await _service.Create(viagem, token);

            return CreatedAtAction(
                nameof(GetById),
                new { id = viagemCriada.Id },
                viagem);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] Viagem viagem,
            CancellationToken token = default
            )
        {
            var result = await _service.Update(id, viagem, token);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var viagem = await _service.FindAll(token);
            return Ok(viagem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken token = default)
        {
            var viagem = await _service.GetById(id, token);
            return Ok(viagem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken token = default)
        {
            await _service.Delete(id, token);
            return NoContent();
        }
    }
} 
