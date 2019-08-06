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
    public class ICDController : Controller
    {
        private IICDRepository _repository;

        public ICDController(IICDRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<ICD>> Get() => await _repository.Get();

        [HttpPost("search")]
        public async Task<ICD> Search(ICD icd) => await _repository.Search(icd);
    }
}