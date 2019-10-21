using hNext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Models
{
    public class RecordTemplateEditorViewModel
    {
        public IEnumerable<Hospital> Hospitals { get; set; }
        public RecordTemplate RecordTemplate { get; set; }
    }
}
