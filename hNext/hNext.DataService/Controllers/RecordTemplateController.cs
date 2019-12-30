using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hNext.IRepository;
using hNext.Model;

namespace hNext.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordTemplateController : ControllerBase
    {
        private IRepository<RecordTemplate> _repository;

        public RecordTemplateController(IRepository<RecordTemplate> repository) => _repository = repository;
    }
}