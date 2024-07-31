using AutoMapper;
using Book.Store.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using DomainAssunto = Book.Store.Business.Domain.Assunto;
using EntityAssunto = Book.Store.Data.Entities.Assunto;

namespace Book.Store.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssuntosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAssuntoRepository _assuntoRepository;

        public AssuntosController(IMapper mapper, IAssuntoRepository assuntoRepository)
        {
            _mapper = mapper;
            _assuntoRepository = assuntoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<DomainAssunto>> Get()
        {
            var assuntos = await _assuntoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DomainAssunto>>(assuntos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DomainAssunto>> Get(int id)
        {
            var assunto = await _assuntoRepository.GetByIdAsync(id);
            if (assunto == null)
            {
                return NotFound();
            }
            return _mapper.Map<DomainAssunto>(assunto);
        }

        [HttpPost]
        public async Task<ActionResult<DomainAssunto>> Post(DomainAssunto assunto)
        {
            var entity = _mapper.Map<EntityAssunto>(assunto);
            await _assuntoRepository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.AssuntoCod }, _mapper.Map<DomainAssunto>(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DomainAssunto assunto)
        {
            if (id != assunto.Id)
            {
                return BadRequest();
            }

            var entity = _mapper.Map<EntityAssunto>(assunto);
            await _assuntoRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _assuntoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}