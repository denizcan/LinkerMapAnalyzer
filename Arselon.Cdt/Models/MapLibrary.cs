using System;
using System.Collections.Generic;

namespace Arselon.Cdt.Models
{
    public class MapLibrary
    {
        public string Name { get; set; }
        public Dictionary<string, MapEntry> Entries { get; } = new();

    }
}
