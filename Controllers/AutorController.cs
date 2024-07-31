using AutoMapper;
using Book.Store.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using DomainAutor = Book.Store.Business.Domain.Autor;
using EntityAutor = Book.Store.Data.Entities.Autor;

namespace Book.Store.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAutorRepository _autorRepository;

        public AutoresController(IMapper mapper, IAutorRepository autorRepository)
        {
            _mapper = mapper;
            _autorRepository = autorRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<DomainAutor>> Get()
        {
            var autores = await _autorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DomainAutor>>(autores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DomainAutor>> Get(int id)
        {
            var autor = await _autorRepository.GetByIdAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return _mapper.Map<DomainAutor>(autor);
        }

        [HttpPost]
        public async Task<ActionResult<DomainAutor>> Post(DomainAutor autor)
        {
            var entity = _mapper.Map<EntityAutor>(autor);
            await _autorRepository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CodAu }, _mapper.Map<DomainAutor>(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DomainAutor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest();
            }

            var entity = _mapper.Map<EntityAutor>(autor);
            await _autorRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _autorRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}