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
    public class DepartmentsController : Controller
    {
        private IDepartmentRepository _repository;
        private IRepository<DepartmentPhone> _phoneRepository;
        private IRepository<DepartmentEmail> _emailRepository;
        private IDepartmentSpecialtyRepository _specialtyRepository;
        private IGetter<Specialty> _specialtyGetter;

        public DepartmentsController(IDepartmentRepository repository, IDepartmentSpecialtyRepository specialtyRepository,
            IRepository<DepartmentPhone> phoneRepository, IRepository<DepartmentEmail> emailRepository, IGetter<Specialty> specialtyGetter)
        {
            _repository = repository;
            _specialtyRepository = specialtyRepository;
            _phoneRepository = phoneRepository;
            _emailRepository = emailRepository;
            _specialtyGetter = specialtyGetter;
        }

        [HttpGet]
        public async Task<IEnumerable<Department>> Get() => await _repository.Get();

        [HttpGet("{id:int}")]
        public async Task<Department> Get(int id) => await _repository.Get(id);

        [HttpPost("exists")]
        public async Task<IActionResult> Exists(Department department)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Exists(department));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Department department)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(department));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, Department department)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != department.Id || ! await _repository.Exists(id))
            {
                return BadRequest();
            }

            return Ok(await _repository.Put(department));
        }

        [HttpPost("{id:int}/emails")]
        public async Task<IActionResult> AddEmail(int id, DepartmentEmail email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (email.DepartmentId != id)
            {
                return BadRequest();
            }

            var department = await _repository.Get(id);
            department.Emails.Add(email);
            department = await _repository.Put(department);
            return Ok(email);
        }

        [HttpDelete("{departmentId:int}/emails/{emailId:long}")]
        public async Task<IActionResult> DeleteEmail(int departmentId, long emailId)
        {
            var department = await _repository.Get(departmentId);
            if (department == null)
            {
                return BadRequest();
            }

            var email = department.Emails.SingleOrDefault(e => e.EmailId == emailId);
            if (email == null)
            {
                return BadRequest();
            }

            return Ok(await _emailRepository.Delete(departmentId, emailId));
        }

        [HttpPost("{id:int}/phones")]
        public async Task<IActionResult> AddPhone(int id, DepartmentPhone departmentPhone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (departmentPhone.DepartmentId != id)
            {
                return BadRequest();
            }

            var department = await _repository.Get(id);
            department.Phones.Add(departmentPhone);
            department = await _repository.Put(department);
            return Ok(departmentPhone);
        }

        [HttpDelete("{departmentId:int}/phones/{phoneId:long}")]
        public async Task<IActionResult> DeletePhone(int departmentId, long phoneId)
        {
            var department = await _repository.Get(departmentId);
            if (department == null)
            {
                return BadRequest();
            }
            var phone = department.Phones.SingleOrDefault(p => p.PhoneId == phoneId);
            if (phone == null)
            {
                return BadRequest();
            }

            return Ok(await _phoneRepository.Delete(departmentId, phoneId));
        }

        [HttpPost("{id:int}/specialties/{specialtyId:int}")]
        public async Task<IActionResult> AddSpecialty(int id, int specialtyId)
        {
            if(! await _repository.Exists(id) || ! await _specialtyGetter.Exists(specialtyId))
            {
                return BadRequest();
            }

            return Ok(await _specialtyRepository.Post(new DepartmentSpecialty { DeparmentId = id, SpecialtyId = specialtyId }));
        }

        [HttpDelete("{id:int}/specialties/{specialtyId:int}")]
        public async Task<IActionResult> DeleteSpecialty(int id, int specialtyId)
        {
            if(! await _specialtyRepository.Exists(id, specialtyId))
            {
                return BadRequest();
            }

            return Ok(await _specialtyRepository.Delete(id, specialtyId));
        }
    }
}