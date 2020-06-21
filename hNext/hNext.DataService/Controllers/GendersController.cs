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
    public class GendersController : ControllerBase
    {
        private IRepository<Gender> _repository;

        public GendersController(IRepository<Gender> repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<Gender>> Get() => await _repository.Get(); 
    }
}
