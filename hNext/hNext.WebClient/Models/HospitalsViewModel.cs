using hNext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Models
{
    public class HospitalsViewModel
    {
        public Hospital Hospital { get; set; }

        public IEnumerable<PropertyType> PropertyTypes { get; set; }
        public IEnumerable<HospitalType> HospitalTypes { get; set; }
        public IEnumerable<Country> Countries { get; set; }
    }
}
