using hNext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Models
{
    public class PhoneEditorViewModel
    {
        public IEnumerable<PhoneType> PhoneTypes { get; set; }
        public Phone Phone { get; set; }
    }
}
