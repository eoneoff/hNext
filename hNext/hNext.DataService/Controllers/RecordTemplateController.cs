using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hNext.IRepository;
using hNext.Model;

namespace hNext.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordTemplateController : ControllerBase
    {
        private IPoster<RecordTemplate> _repository;

        public RecordTemplateController (IPoster<RecordTemplate> repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<RecordTemplate>> Get() => await _repository.Get();

        [HttpGet("{id:int}")]
        public async Task<RecordTemplate> Get(int id) => await _repository.Get(id);

        [HttpPost("{id:int}")]
        public async Task<IActionResult> Post(RecordTemplate recordTemplate)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(recordTemplate));
        }
    }
}