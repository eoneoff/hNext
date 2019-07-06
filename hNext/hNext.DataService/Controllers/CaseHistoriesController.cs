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

        public CaseHistoriesController(ICaseHistoryRepository repository)
        {
            _repository = repository;
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
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(history));
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, CaseHistory history)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(! await _repository.Exists(id) || history.Id != id)
            {
                return BadRequest();
            }

            return Ok(await _repository.Put(history));
        }
    }
}