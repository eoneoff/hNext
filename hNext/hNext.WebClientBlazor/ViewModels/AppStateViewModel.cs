using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClientBlazor.ViewModels
{
    public class AppStateViewModel
    {
        public Dictionary<string, object> State { get; private set; } = new Dictionary<string, object>(); 
    }
}
