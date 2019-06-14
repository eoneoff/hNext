using hNext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Models
{
    public class DoctorViewModel
    {
        public Doctor Doctor { get; set; }
        public DoctorSearchModel Search { get; set; }

        public IEnumerable<Specialty> Specialties { get; set; }
    }
}
