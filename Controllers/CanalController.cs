using AutoMapper;
using Book.Store.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using DomainCanal = Book.Store.Business.Domain.Canal;
using EntityCanal = Book.Store.Data.Entities.Canal;

namespace Book.Store.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanalController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICanalRepository _canalRepository;

        public CanalController(IMapper mapper, ICanalRepository canalRepository)
        {
            _mapper = mapper;
            _canalRepository = canalRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<DomainCanal>> Get()
        {
            var canais = await _canalRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DomainCanal>>(canais);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DomainCanal>> Get(int id)
        {
            var canal = await _canalRepository.GetByIdAsync(id);
            if (canal == null)
            {
                return NotFound();
            }
            return _mapper.Map<DomainCanal>(canal);
        }

        [HttpPost]
        public async Task<ActionResult<DomainCanal>> Post(DomainCanal canal)
        {
            var entity = _mapper.Map<EntityCanal>(canal);
            await _canalRepository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CanalCod }, _mapper.Map<DomainCanal>(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DomainCanal canal)
        {
            if (id != canal.Id)
            {
                return BadRequest();
            }

            var entity = _mapper.Map<EntityCanal>(canal);
            await _canalRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _canalRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}