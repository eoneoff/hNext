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
        private IRepository<PatientDiagnosys> _diagnosysRepository;

        public PatientsController(IPatientsRepository repository,
                                    IRepository<PatientDiagnosys> diagnosysRepository)
        {
            _repository = repository;
            _diagnosysRepository = diagnosysRepository;
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
        public async Task<IActionResult> Post(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(patient));
        }

        // PUT api/<controller>/5
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, Patient patient)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!await _repository.Exists(id))
            {
                return BadRequest();
            }

            return Ok(await _repository.Put(patient));
        }

        [HttpGet("{id:long}/diagnoses")]
        public async Task<IEnumerable<PatientDiagnosys>> GetDiagnoses(long id) =>
            await _repository.GetDiagnoses(id);

        [HttpPost("{id:long}/diagnoses/")]
        public async Task<IActionResult> AddDiagnosys(long id, PatientDiagnosys diagnosys)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(! await _repository.Exists(id) || await _diagnosysRepository.Exists(id, diagnosys.DiagnosysId))
            {
                return BadRequest();
            }

            diagnosys.PatientId = id;

            return Ok(await _diagnosysRepository.Post(diagnosys));
        }

        [HttpDelete("{id:long}/diagnoses/{diagnosysId:long}")]
        public async Task<IActionResult> RemoveDiagnosys(long id, long diagnosysId)
        {
            if (!await _diagnosysRepository.Exists(id, diagnosysId))
            {
                return BadRequest();
            }

            return Ok(await _diagnosysRepository.Delete(id, diagnosysId));
        }
    }
}
