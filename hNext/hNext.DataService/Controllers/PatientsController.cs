using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hNext.IRepository;
using hNext.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hNext.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : Controller
    {
        private IPatientsRepository _repository;

        public PatientsController(IPatientsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<Patient>> Get() => await _repository.Get();

        // GET api/<controller>/5
        [HttpGet("{id:int}")]
        public async Task<Patient> Get(int id) => await _repository.Get(id);

        //api/<controller>/search
        [Route("search")]
        public async Task<IEnumerable<Patient>> Search(PatientSearchModel model) =>
            await _repository.SearchPatients(model);

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Patient>> Post(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _repository.Post(patient);
        }

        // PUT api/<controller>/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Patient>> Put(long id, Patient patient)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!await _repository.Exists(id))
            {
                return BadRequest();
            }

            return await _repository.Put(patient);
        }
    }
}
