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
    public class PeopleController : Controller
    {
        private IPersonRepository _repository;

        public PeopleController(IPersonRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<Person>> Get() => await _repository.Get();

        [HttpGet("{id:int}")]
        public async Task<Person> Get(int id) => await _repository.Get(id);

        [HttpPost("exists")]
        public async Task<Person> Exists(Person person) => await _repository.Exists(person);

        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(person));
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, Person person)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (! await _repository.Exists(id))
            {
                return BadRequest();
            }

            return Ok(await _repository.Put(person));
        }
    }
}