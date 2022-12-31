using System;
using System.Collections.Generic;

namespace Arselon.Cdt.Models
{
    public class MapModule
    {
        public string Name { get; set; }
        public Dictionary<string, MapEntry> Entries { get; } = new();
    }
}
