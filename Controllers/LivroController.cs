using AutoMapper;
using Book.Store.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using DomainLivro = Book.Store.Business.Domain.Livro;
using EntityLivro = Book.Store.Data.Entities.Livro;

namespace Book.Store.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILivroRepository _livroRepository;

        public LivrosController(IMapper mapper, ILivroRepository livroRepository)
        {
            _mapper = mapper;
            _livroRepository = livroRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<DomainLivro>> Get()
        {
            var livros = await _livroRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DomainLivro>>(livros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DomainLivro>> Get(int id)
        {
            var livro = await _livroRepository.GetByIdAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return _mapper.Map<DomainLivro>(livro);
        }

        [HttpPost]
        public async Task<ActionResult<DomainLivro>> Post(DomainLivro livro)
        {
            var entity = _mapper.Map<EntityLivro>(livro);
            await _livroRepository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.LivroCod }, _mapper.Map<DomainLivro>(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DomainLivro livro)
        {
            if (id != livro.Id)
            {
                return BadRequest();
            }

            var entity = _mapper.Map<EntityLivro>(livro);
            await _livroRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _livroRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
