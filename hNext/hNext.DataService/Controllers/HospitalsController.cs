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
    public class HospitalsController : Controller
    {
        private IHospitalRepository _repository;
        private IRepository<HospitalEmail> _emailRepository;
        private IRepository<HospitalPhone> _phoneRepository;

        public HospitalsController(IHospitalRepository repository, IRepository<HospitalEmail> emailRepository,
            IRepository<HospitalPhone> phoneRepository)
        {
            _repository = repository;
            _emailRepository = emailRepository;
            _phoneRepository = phoneRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Hospital>> Get() => await _repository.Get();

        [HttpGet("{id:int}")]
        public async Task<Hospital> Get(int id) => await _repository.Get(id);

        [HttpGet("exists/{name}")]
        public async Task<bool> Exists(string name) => await _repository.Exists(name);

        [HttpPost]
        public async Task<IActionResult> Post(Hospital hospital)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(hospital));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, Hospital hospital)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(hospital.Id != id || ! await _repository.Exists(id))
            {
                return BadRequest();
            }

            return Ok(await _repository.Put(hospital));
        }

        [HttpPost("{id:int}/emails")]
        public async Task<IActionResult> AddEmail(int id, HospitalEmail hospitalEmail)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(hospitalEmail.HospitalId != id)
            {
                return BadRequest();
            }

            var hospital = await _repository.Get(id);
            hospital.Emails.Add(hospitalEmail);
            hospital = await _repository.Put(hospital);
            return Ok(hospitalEmail);
        }

        [HttpDelete("{hospitalId:int}/emails/{emailId:long}")]
        public async Task<IActionResult> DeleteEmail(int hospitalId, long emailId)
        {
            var hospital = await _repository.Get(hospitalId);
            if(hospital == null)
            {
                return BadRequest();
            }

            var email = hospital.Emails.SingleOrDefault(e => e.EmailId == emailId);
            if(email == null)
            {
                return BadRequest();
            }

            return Ok(await _emailRepository.Delete(hospitalId, emailId));
        }

        [HttpPost("{id:int}/phones")]
        public async Task<IActionResult> AddPhone(int id, HospitalPhone hospitalPhone)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(hospitalPhone.HospitalId != id)
            {
                return BadRequest();
            }

            var hospital = await _repository.Get(id);
            hospital.Phones.Add(hospitalPhone);
            hospital = await _repository.Put(hospital);
            return Ok(hospitalPhone);
        }

        [HttpDelete("{hospitalId:int}/phones/{phoneId:long}")]
        public async Task<IActionResult> DeletePhone(int hospitalId, long phoneId)
        {
            var hospital = await _repository.Get(hospitalId);
            if(hospital == null)
            {
                return BadRequest();
            }
            var phone = hospital.Phones.SingleOrDefault(p => p.PhoneId == phoneId);
            if(phone == null)
            {
                return BadRequest();
            }

            return Ok(await _phoneRepository.Delete(hospitalId, phoneId));
        }
    }
}