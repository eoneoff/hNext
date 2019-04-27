using hNext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Models
{
    public class DocumentEditorViewModel
    {
        public IEnumerable<DocumentType> DocumentTypes { get; set; }
        public Document Document { get; set; }
    }
}
