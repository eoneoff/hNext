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
    public class EmailsController : Controller
    {
        private IEmailRepository _repository;

        public EmailsController(IEmailRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<Email>> Get() => await _repository.Get();

        [HttpGet("{id:int}")]
        public async Task<Email> Get(long id) => await _repository.Get(id);

        [HttpPost]
        public async Task<IActionResult> Post(Email email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(email));
        }

        [HttpGet("exists/{address}")]
        public async Task<Email> Exists(string address) => await _repository.Exists(address);

        [HttpGet("belongtoothers/{id:long}")]
        public async Task<bool> BelongToOthers(long id) => await _repository.BelongToOthers(id);

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, Email email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _repository.Exists(id))
            {
                return BadRequest();
            }

            return Ok(await _repository.Put(email));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Email email = await _repository.Delete(id);
            if (email == null)
                return BadRequest();
            else
                return Ok(email);
        }
    }
}