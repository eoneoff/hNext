using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hNext.IRepository;
using hNext.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hNext.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardiansController : Controller
    {
        private IGuardianRepository _repository;

        public GuardiansController(IGuardianRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<GuardianWard>> Get() => await _repository.Get();

        [HttpGet("{wardId:long}/{guardianId:long}")]
        public async Task<GuardianWard> Get(long wardId, long guardianId) =>
            await _repository.Get(wardId, guardianId);

        [HttpGet("{wardId:long}/exists/{guardianId:long}")]
        public async Task<bool> Exists(long wardId, long guardianId) => await _repository.Exists(wardId, guardianId);

        [HttpPost("{wardId:long}/exists")]
        public async Task<IActionResult> Exists(long wardId, GuardianWard guardian)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(guardian.WardId != wardId)
            {
                return BadRequest();
            }

            return Ok(await _repository.Exists(guardian));
        }

        [HttpPost]
        public async Task<IActionResult> Post(GuardianWard guardian)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(guardian));
        }

        [HttpPut("{wardId:long}/{guardianId:long}")]
        public async Task<IActionResult> Put(long wardId, long guardianId, GuardianWard guardian)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!await _repository.Exists(wardId, guardianId))
            {
                return BadRequest();
            }

            return Ok(await _repository.Put(guardian));
        }

        [HttpDelete("{wardId:long}/{guardianId:long}")]
        public async Task<IActionResult> Delete(long wardId, long guardianId)
        {
            if(!await  _repository.Exists(wardId, guardianId))
            {
                return BadRequest();
            }

            return Ok(await _repository.Delete(wardId, guardianId));
        }
    }
}