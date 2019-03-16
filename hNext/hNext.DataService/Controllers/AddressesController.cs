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
    public class AddressesController : ControllerBase
    {
        private IAddressRepository _repository;

        public AddressesController(IAddressRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<Address>> Get() => await _repository.Get();

        [HttpGet("{id:int}")]
        public async Task<Address> Get(long id) => await _repository.Get(id);

        [HttpPost("exists")]
        public async Task<Address> Exists(Address address) => await _repository.Exists(address);
    }
}