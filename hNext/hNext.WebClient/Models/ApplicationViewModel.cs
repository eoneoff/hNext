using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Models
{
    public class ApplicationViewModel
    {
        public Dictionary<string, string> Tabs { get; set; } = new Dictionary<string, string>();
    }
}
