using hNext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Models
{
    public class GuardianEditorViewModel
    {
        public IEnumerable<Person> FountPeople { get; set; }
        public GuardianWard Guardian { get; set; }
    }
}
