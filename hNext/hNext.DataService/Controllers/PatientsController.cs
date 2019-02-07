using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hNext.IRepository;
using hNext.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hNext.DataService.Controllers
{
    [Route("api/[controller]")]
    public class PatientsController : Controller
    {
        private IPatientsRepository _repository;

        public PatientsController(IPatientsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<controller>
        [ProducesResponseType(200, Type = typeof(IEnumerable<Patient>))]
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _repository.Get());

        // GET api/<controller>/5
        [ProducesResponseType(200, Type = typeof(Patient))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _repository.Get(id));

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
