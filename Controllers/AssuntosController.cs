using Book.Store.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Book.Store.Business.Domain;

namespace Book.Store.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssuntosController : ControllerBase
    {
        private readonly IAssuntoService _assuntoService;

        public AssuntosController(IAssuntoService assuntoService)
        {
            _assuntoService = assuntoService;
        }

        [HttpGet]
        public async Task<IEnumerable<Assunto>?> Get()
        {
            return await _assuntoService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Assunto>> Get(int id)
        {
            var assunto = await _assuntoService.Get(id);
            if (assunto == null)
            {
                return NotFound();
            }
            return assunto;
        }

        [HttpPost]
        public async Task<ActionResult<Assunto>> Post(Assunto assunto)
        {
            var _assunto = await _assuntoService.Post(assunto);
            return CreatedAtAction(nameof(Get), new { id = _assunto.Id }, _assunto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Assunto assunto)
        {
            if (id != assunto.Id)
            {
                return BadRequest();
            }

            await _assuntoService.Put(id, assunto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _assuntoService.Delete(id);
            return NoContent();
        }
    }
}