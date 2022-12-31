using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arselon.Cdt.Models
{
    public class MapEntry
    {
        private MapSection _section;
        private MapModule _module;
        private MapLibrary _library;

        public string Name { get; set; }
        public string Path { get; set; }
        public uint Origin { get; set; }
        public uint Length { get; set; }

        public MapSection Section
        { 
            get
            {
                return _section;
            }

            set
            {
                _section = value;
                value.Entries.Add(Path, this);
            }
        }

        public MapModule Module
        { 
            get
            {
                return _module;
            }
            set
            {
                _module = value;
                Path = value.Name + "/" + Name;
                value.Entries.Add(Path, this);
            }
        }

        public MapLibrary Library 
        {
            get { return _library; }
            set
            {
                _library = value;
                _library.Entries.Add(Path, this);
            }
        }
    }
}
