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
    public class RegionsController : Controller
    {
        private IRegionsRepository _repository;

        public RegionsController(IRegionsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Region>> Get()
        {
            return await _repository.Get();
        }

        [HttpGet("{id:int}")]
        public async Task<Region> Get(int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet("{id:int}/districts")]
        public async Task<IEnumerable<District>> GetDistricts(int id)
        {
            return await _repository.GetDistricts(id);
        }

        [HttpGet("{id:int}/cities")]
        public async Task<IEnumerable<City>> GetCities(int id)
        {
            return await _repository.GetCities(id);
        }
    }
}