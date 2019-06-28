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
    public class DoctorsController : Controller
    {
        private IDoctorRepository _repository;
        private IDoctorSpecialtyRepository _specialtyRepository;
        private IGetter<Specialty> _specialtyGetter;
        private IDoctorPositionRepository _positionRepository;
        private IRepository<Diploma> _diplomaRepository;

        public DoctorsController(IDoctorRepository repository,
                                IDoctorSpecialtyRepository specialtyRepository,
                                IGetter<Specialty> specialtyGetter,
                                IDoctorPositionRepository positionRepository,
                                IRepository<Diploma> diplomaRepository)
        {
            _repository = repository;
            _specialtyRepository = specialtyRepository;
            _specialtyGetter = specialtyGetter;
            _positionRepository = positionRepository;
            _diplomaRepository = diplomaRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Doctor>> Get() => await _repository.Get();

        [HttpGet("{id:int}")]
        public async Task<Doctor> Get(int id) => await _repository.Get(id);

        [Route("search")]
        public async Task<IEnumerable<Doctor>> Search(DoctorSearchModel model) => await _repository.SearchDoctors(model);

        [HttpPost]
        public async Task<IActionResult> Post(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(doctor));
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (doctor.Id != id)
            {
                return BadRequest();
            }

            return Ok(await _repository.Put(doctor));
        }

        [HttpGet("{id:long}/specialties")]
        public async Task<IEnumerable<Specialty>> Specialties(long id) => await _repository.Specialties(id);

        [HttpPost("{id:long}/specialties/")]
        public async Task<IActionResult> AddSpecialty(long id, DoctorSpecialty specialty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _repository.Exists(id) || !await _specialtyGetter.Exists(specialty.SpecialtyId)
                || specialty.DoctorId != id)
            {
                return BadRequest();
            }

            return Ok(await _specialtyRepository.Post(specialty));
        }

        [HttpPut("{id:long}/specialties/{doctorSpecialtyId:long}")]
        public async Task<IActionResult> EditSpecialty(long id, long doctorSpecialtyId, DoctorSpecialty specialty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _repository.Exists(id) || !await _specialtyGetter.Exists(specialty.SpecialtyId)
                || specialty.DoctorId != id || doctorSpecialtyId != specialty.Id)
            {
                return BadRequest();
            }

            return Ok(await _specialtyRepository.Put(specialty));
        }

        [HttpDelete("{id:long}/specialties/{specialtyId:int}")]
        public async Task<IActionResult> DeleteSpecialty(long id, int specialtyId)
        {
            var specialty = await _specialtyRepository.Exists(id, specialtyId);

            if (specialty == null)
            {
                return BadRequest();
            }

            return Ok(await _specialtyRepository.Delete(specialty.Id));
        }

        [HttpGet("{id:long}/specialties/{specialtyId:int}/exists")]
        public async Task<DoctorSpecialty> SpecialtyExists(long id, int specialtyId) =>
            await _specialtyRepository.Exists(id, specialtyId);

        [HttpPost("{id:long}/positions/")]
        public async Task<IActionResult> AddPosition(long id, DoctorPosition position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _positionRepository.Exists(position) is DoctorPosition)
            {
                return BadRequest();
            }

            position.DoctorId = id;

            return Ok(await _positionRepository.Post(position));
        }

        [HttpPut("{id:long}/positions/{positionId:long}")]
        public async Task<IActionResult> EditPosition(long id, long positionId, DoctorPosition position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _positionRepository.Exists(positionId) || position.DoctorId != id)
            {
                return BadRequest();
            }

            return Ok(await _positionRepository.Put(position));
        }

        [HttpDelete("{id:long}/positions/{positionId:long}")]
        public async Task<IActionResult> DeletePosition(long id, long positionId)
        {
            var position = await _positionRepository.Get(positionId);

            if (position == null || position.DoctorId != id)
            {
                return BadRequest();
            }

            return Ok(await _positionRepository.Delete(positionId));
        }

        [HttpPost("{id:long}/diplomas/")]
        public async Task<IActionResult> AddDiplomaToDoctor(long id, Diploma diploma)
        {
            diploma.DoctorId = id;

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _diplomaRepository.Post(diploma));
        }

        [HttpPut("{id:long}/diplomas/{diplomaId:long}")]
        public async Task<IActionResult> EditDiploma(long id, long diplomaId, Diploma diploma)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(diploma.Id != diplomaId || diploma.DoctorId != id
                || !await _diplomaRepository.Exists(diplomaId))
            {
                return BadRequest();
            }

            return Ok(await _diplomaRepository.Put(diploma));
        }

        [HttpDelete("{id:long}/diplomas/{diplomaId:long}")]
        public async Task<IActionResult> DeleteDiploma(long id, long diplomaId)
        {
            if(! (await _diplomaRepository.Get(diplomaId) is Diploma diploma) || diploma.DoctorId != id)
            {
                return BadRequest();
            }

            return Ok(await _diplomaRepository.Delete(diplomaId));
        }
    }
}