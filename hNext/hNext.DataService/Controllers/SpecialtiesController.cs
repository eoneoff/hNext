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
    public class SpecialtiesController : Controller
    {
        private IGetter<Specialty> _getter;

        public SpecialtiesController(IGetter<Specialty> getter) => _getter = getter;

        [HttpGet]
        public async Task<IEnumerable<Specialty>> Get() => await _getter.Get();
    }
}