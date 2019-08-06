using hNext.Infrastructure;
using hNext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Models
{
    public class CaseHistoryViewModel
    {
        public CaseHistory CaseHistory { get; set; }
        public Dictionary<string, string> Tabs { get; set; } = new Dictionary<string, string>();
        public UniqueList<string> Modules { get; set; } = new UniqueList<string>();
    }
}
