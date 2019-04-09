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
        private IRepository<PersonPhone> _phoneRepository;

        public PeopleController(IPersonRepository repository, IRepository<PersonPhone> phoneRepository)
        {
            _repository = repository;
            _phoneRepository = phoneRepository;
        }

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

        [HttpPost("addphone")]
        public async Task<IActionResult> AddPhone(PersonPhone personPhone)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _repository.Get(personPhone.PersonId);
            person.Phones.Add(personPhone);
            person = await _repository.Put(person);
            return Ok(personPhone);
        }

        [HttpDelete("{personId:long}/phone/{phoneId:long}")]
        public async Task<IActionResult> RemovePhone(long personId, long phoneId)
        {
            var person = await _repository.Get(personId);
            if(person == null)
            {
                return BadRequest();
            }

            var phone = person.Phones.SingleOrDefault(p => p.Phone.Id == phoneId);

            if(phone == null)
            {
                return BadRequest();
            }

            return Ok(await _phoneRepository.Delete(personId, phoneId));
        }
    }
}