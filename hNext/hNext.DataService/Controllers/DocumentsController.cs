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
    public class DocumentsController : Controller
    {
        private IDocumentsRepository _repository;

        public DocumentsController(IDocumentsRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<Document>> Get() => await _repository.Get();

        [HttpGet("{id:long}")]
        public async Task<Document> Get(long id) => await _repository.Get(id);

        [HttpGet("exists/{documentTypeId:int}/{number}")]
        public async Task<bool> Exists(int documentTypeId, string number) =>
            await _repository.Exists(documentTypeId, number);

        [HttpPost]
        public async Task<IActionResult> Post(Document document)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(document));
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, Document document)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!await _repository.Exists(id))
            {
                return BadRequest();
            }

            return Ok(await _repository.Put(document));
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            if(! await _repository.Exists(id))
            {
                return BadRequest();
            }
            else
            {
                return Ok(await _repository.Delete(id));
            }
        }
    }
}