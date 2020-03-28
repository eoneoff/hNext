using System;
using System.Collections;
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
    public class DrugsController : Controller
    {
        private IDrugRepository _repository;

        public DrugsController(IDrugRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<Drug>> Get() => await _repository.Get();

        [HttpGet("{searchLine}")]
        public async Task<IEnumerable<Drug>> Search(string searchLine) => await _repository.Search(searchLine);
    }
}