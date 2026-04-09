using Microsoft.AspNetCore.Mvc;
using WebApplication2.Application;
using WebApplication2.Domain.Entities;


namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class DestinoController : ControllerBase
    {
        private readonly DestinoService _service;

        public DestinoController(DestinoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var result = await _service.FindAll(token);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken token)
        {
            var result = await _service.GetById(id, token);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Destino destino, CancellationToken token)
        {
            var result = await _service.Create(destino, token);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Destino destino, CancellationToken token)
        {
            var result = await _service.Update(id, destino, token);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken token)
        {
            await _service.Delete(id, token);
            return NoContent();
        }
    }
}
