using hNext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Models
{
    public class PatientSearchViewModel
    {
        public PatientSearchModel Search { get; set; }
        public IEnumerable<Patient> Found { get; set; }
    }
}
