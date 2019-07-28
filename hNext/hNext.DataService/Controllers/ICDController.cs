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
        private IGetter<ICD> _repository;

        public ICDController(IGetter<ICD> repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<ICD>> Get() => (await _repository.Get());
    }
}