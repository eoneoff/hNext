using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hNext.IRepository;
using hNext.Model;

namespace hNext.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : Controller
    {
        private IPhoneRepository _repository;

        public PhonesController(IPhoneRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<Phone>> Get() => await _repository.Get();

        [HttpGet("{id:int}")]
        public async Task<Phone> Get(long id) => await _repository.Get(id);

        [HttpPost]
        public async Task<IActionResult> Post(Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(phone));
        }

        [HttpGet("exists/{number}")]
        public async Task<Phone> Exists(string number) => await _repository.Exists(number.Replace("$$plus$$", "+"));

        [HttpGet("belongtoothers/{id:long}")]
        public async Task<bool> BelongToOthers(long id) => await _repository.BelongToOthers(id);

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _repository.Exists(id))
            {
                return BadRequest();
            }

            return Ok(await _repository.Put(phone));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(long id)
        {
            Phone phone = await _repository.Delete(id);
            if (phone == null)
                return BadRequest();
            else
                return Ok(phone);
        }

    }
}