using System;
using System.Collections.Generic;
using Arselon.Cdt.Models;

namespace Arselon.Cdt.Extractor
{
    public class Extractor
    {
        protected Dictionary<string, MapModule> Modules { get; } = new();
        protected Dictionary<string, MapSection> Sections { get; } = new();
        protected Dictionary<string, MapEntry> Entries { get; } = new();
        public Dictionary<string, MapLibrary> Libraries { get; } = new();

        protected MapEntry GetEntry(string moduleName, string name)
        {
            string path = moduleName + "/" + name;
            MapEntry entry;
            if (Entries.TryGetValue(path, out entry))
                return entry;

            entry = new MapEntry();
            entry.Name = name;
            entry.Path = path;
            entry.Module = GetModule(moduleName);
            Entries.Add(path, entry);

            return entry;
        }

        protected MapSection GetSection(string name)
        {
            MapSection section;
            if (Sections.TryGetValue(name, out section))
                return section;

            section = new MapSection();
            section.Name = name;
            Sections.Add(name, section);
            return section;
        }

        protected MapModule GetModule(string name)
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
