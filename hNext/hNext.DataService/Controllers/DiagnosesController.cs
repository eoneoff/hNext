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
    public class DiagnosesController : Controller
    {
        private IPoster<Diagnosys> _repository;

        public DiagnosesController(IPoster<Diagnosys> repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<Diagnosys>> Get() => await _repository.Get();

        [HttpGet("{id:long}")]
        public async Task<Diagnosys> Get(long id) => await _repository.Get(id);

        [HttpPost]
        public async Task<IActionResult> Post(Diagnosys diagnosys)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(diagnosys));
        }
    }
}