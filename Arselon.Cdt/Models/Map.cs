using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arselon.Cdt.Models
{
    public class Map        
    {
        public Dictionary<string, MapModule> Modules { get; } = new();
        public Dictionary<string, MapSection> Sections { get; } = new();
        public Dictionary<string, MapEntry> Entries { get; } = new();
        public Dictionary<string, MapLibrary> Libraries { get; } = new();

        public MapEntry GetEntry(string name)
        {
            MapEntry entry;
            if (Entries.TryGetValue(name, out entry))
                return entry;

            entry = new MapEntry();
            entry.Name = name;
            Entries.Add(name, entry);
            return entry;
        }

        public MapSection GetSection(string name)
        {
            MapSection section;
            if (Sections.TryGetValue(name, out section))
                return section;

            section = new MapSection();
            section.Name = name;
            Sections.Add(name, section);
            return section;
        }

        public MapModule GetModule(string name)
        {
            MapModule module;
            if (Modules.TryGetValue(name, out module))
                return module;

            module = new MapModule();
            module.Name = name;
            Modules.Add(name, module);
            return module;
        }

        public MapLibrary GetLibrary(string name)
        {
            MapLibrary library;
            if (Libraries.TryGetValue(name, out library))
                return library;

            library = new MapLibrary();
            library.Name = name;
            Libraries.Add(name, library);
            return library;
        }
    }
}
