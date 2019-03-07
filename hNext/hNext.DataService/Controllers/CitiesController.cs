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
    public class CitiesController : Controller
    {
        private ICityRepository _repository;

        public CitiesController(ICityRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<City>> Get() => await _repository.Get();

        [HttpGet("{id:int}")]
        public async Task<City> Get(int id) => await _repository.Get(id);

        [HttpGet("{id:int}/streets")]
        public async Task<IEnumerable<Street>> GetStreets(int id) => await _repository.GetStreets(id);
    }
}