using System;
using System.Collections;
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
    public class CountriesController : Controller
    {
        private ICountryRepository _repository;

        public CountriesController(ICountryRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<Country>> Get() => await _repository.Get();

        [HttpGet("{id:int}")]
        public async Task<Country> Get(int id) => await _repository.Get(id);

        [HttpGet("{id:int}/regions")]
        public async Task<IEnumerable<Region>> GetRegions(int id) => await _repository.GetRegions(id);

        [HttpGet("{id:int}/cities")]
        public async Task<IEnumerable<City>> GetCities(int id) => await _repository.GetCities(id);
    }
}