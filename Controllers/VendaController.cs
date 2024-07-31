using AutoMapper;
using Book.Store.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using DomainVenda = Book.Store.Business.Domain.Venda;
using EntityVenda = Book.Store.Data.Entities.Venda;

namespace Book.Store.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IMapper _mapper;

        public VendaController(IVendaRepository vendaRepository, IMapper mapper)
        {
            _vendaRepository = vendaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DomainVenda>>> GetAll()
        {
            var entityVendas = await _vendaRepository.GetAllAsync();
            var domainVendas = _mapper.Map<IEnumerable<DomainVenda>>(entityVendas);
            return Ok(domainVendas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DomainVenda>> GetById(int id)
        {
            var entityVenda = await _vendaRepository.GetByIdAsync(id);
            if (entityVenda == null)
            {
                return NotFound();
            }
            var domainVenda = _mapper.Map<DomainVenda>(entityVenda);
            return Ok(domainVenda);
        }

        [HttpPost]
        public async Task<ActionResult<DomainVenda>> Create(DomainVenda domainVenda)
        {
            var entityVenda = _mapper.Map<EntityVenda>(domainVenda);
            await _vendaRepository.AddAsync(entityVenda);
            var createdDomainVenda = _mapper.Map<DomainVenda>(entityVenda);
            return CreatedAtAction(nameof(GetById), new { id = createdDomainVenda.Id }, createdDomainVenda);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DomainVenda domainVenda)
        {
            if (id != domainVenda.Id)
            {
                return BadRequest();
            }

            var entityVenda = _mapper.Map<EntityVenda>(domainVenda);
            await _vendaRepository.UpdateAsync(entityVenda);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vendaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
