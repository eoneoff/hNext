using hNext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hNext.WebClient.Models
{
    public class DoctorPositionEditorViewModel
    {
        public IEnumerable<Position> Positions { get; set; }
        public DoctorPosition Position { get; set; }
    }
}
