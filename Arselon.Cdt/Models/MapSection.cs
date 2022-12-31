using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arselon.Cdt.Models
{
    public class MapSection
    {
        public string Name { get; set; }
        public Dictionary<string, MapEntry> Entries { get; } = new();
    }
}
