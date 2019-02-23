using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hNext.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hNext.Model;

namespace hNext.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : Controller
    {
        private IDistrictsRepository _repository;

        public DistrictsController(IDistrictsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<District>> Get()
        {
            return await _repository.Get();
        }

        [HttpGet("{id:int}")]
        public async Task<District> Get (int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet("{id:int}/cities")]
        public async Task<IEnumerable<City>> GetCities (int id)
        {
            return await _repository.GetCities(id);
        }
    }
}