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
    public class CaseHistoriesController : Controller
    {
        private ICaseHistoryRepository _repository;
        private IRepository<CaseHistoryDiagnosys> _diagnosesRepository;
        private IRepository<CaseHistoryAdmission> _admissionRepository;
        private IRepository<CaseHistoryRecord> _recordsRepository;

        public CaseHistoriesController(ICaseHistoryRepository repository,
                                        IRepository<CaseHistoryDiagnosys> diagnosesRepository,
                                        IRepository<CaseHistoryAdmission> admissionRepository,
                                        IRepository<CaseHistoryRecord> recordsRepository)
        {
            _repository = repository;
            _diagnosesRepository = diagnosesRepository;
            _admissionRepository = admissionRepository;
            _recordsRepository = recordsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CaseHistory>> Get() => await _repository.Get();

        [HttpGet("{id:long}")]
        public async Task<CaseHistory> Get(long id) => await _repository.Get(id);

        [HttpGet("{id:long}/info")]
        public async Task<CaseHistory> Info(long id) => await _repository.Info(id);

        [HttpPost]
        public async Task<IActionResult> Post(CaseHistory history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(history));
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, CaseHistory history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _repository.Exists(id) || history.Id != id)
            {
                return BadRequest();
            }

            return Ok(await _repository.Put(history));
        }

        [HttpPost("{id:long}/diagnoses/")]
        public async Task<IActionResult> AddDiagnosys(long id, CaseHistoryDiagnosys diagnosys)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _repository.Exists(id) || await _diagnosesRepository.Exists(id, diagnosys.DiagnosysId))
            {
                return BadRequest();
            }

            diagnosys.CaseHistoryId = id;

            return Ok(await _diagnosesRepository.Post(diagnosys));
        }

        [HttpDelete("{id:long}/diagnoses/{diagnosysId:long}")]
        public async Task<IActionResult> RemoveDiagnosys(long id, long diagnosysId)
        {
            if (!await _diagnosesRepository.Exists(id, diagnosysId))
            {
                return BadRequest();
            }

            return Ok(await _diagnosesRepository.Delete(id, diagnosysId));
        }

        [HttpPost("{id:long}/admissions/")]
        public async Task<IActionResult> AddAdmission(long id, CaseHistoryAdmission admission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _repository.Exists(id))
            {
                return BadRequest();
            }

            return Ok(await _admissionRepository.Post(admission));
        }

        [HttpPut("{id:long}/admissions/{admissionId:long}")]
        public async Task<IActionResult> EditAdmission(long id, long admissionId, CaseHistoryAdmission admission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _repository.AdmissionExists(id, admissionId))
            {
                return BadRequest();
            }

            return Ok(await _admissionRepository.Put(admission));
        }

        [HttpPost("{id:long}/records")]
        public async Task<IActionResult> AddRecord(long id, CaseHistoryRecord record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _repository.Exists(id))
            {
                return BadRequest();
            }

            record.CaseHistoryId = id;

            return Ok(await _recordsRepository.Post(record));
        }

        [HttpPut("{id:long}/records/{recordId:long}")]
        public async Task<IActionResult> EditRecord(long id, long recordId, CaseHistoryRecord record)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!await _repository.RecordExists(id, recordId))
            {
                return BadRequest();
            }

            return Ok(await _recordsRepository.Put(record));
        }

        [HttpDelete("{id:long}/records/{recordId:long}")]
        public async Task<IActionResult> DeleteRecord(long id, long recordId)
        {
            if(!await _repository.RecordExists(id, recordId))
            {
                return BadRequest();
            }

            return Ok(await _recordsRepository.Delete(recordId));
        }
    }
}