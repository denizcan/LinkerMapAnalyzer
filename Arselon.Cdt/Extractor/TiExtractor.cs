using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arselon.Cdt.Models;
using Arselon.Utility;

namespace Arselon.Cdt.Extractor
{
    public class TiExtractor : Extractor
    {
        public void PrintTerms(string[] terms)
        {
            foreach (var t in terms)
                Debug.Write($"{t}|");
            Debug.WriteLine("");
        }

        public void ExtractMemoryConfiguration(string[] lines)
        {
            var parser = new MapParser(lines);
            parser.SkipTo("MEMORY CONFIGURATION");
            parser.RequireEmpty();
            parser.BeginTable(
                new string[] { "name", "origin", "length", "used", "unused", "attr", "fill" },
                "----------------------  --------  ---------  --------  --------  ----  --------");

            while (!parser.IsEmpty)
            {
                var terms = parser.GetTerms();
                PrintTerms(terms);
            }
        }

        public void ExtractSegmentAllocationMap(string[] lines)
        {
            var parser = new MapParser(lines);
            parser.SkipTo("SEGMENT ALLOCATION MAP");
            parser.RequireEmpty();
            parser.BeginTable(
                new string[] { "run origin", "load origin", "length", "init length", "attrs", "members" },
                "----------  ----------- ---------- ----------- ----- -------");

            while (!parser.IsEmpty)
            {
                var terms = parser.GetTerms();
                PrintTerms(terms);
            }
        }

        int _nonameCounter = 0;

        public void ExtractSectionAllocationMap(string[] lines)
        {
            var parser = new MapParser(lines);
            parser.SkipTo("SECTION ALLOCATION MAP");
            parser.RequireEmpty();
            parser.MoveNext();
            parser.MoveNext();
            parser.MoveNext();
            parser.SetColumnLine("--------  ----  ----------  ----------   ----------------");

            MapLibrary currentLibrary = null;
            while (parser.Current.StartsWith("."))
            {
                Debug.WriteLine(parser.Current);
                parser.MoveNext();
                while (!parser.IsEmpty)
                {
                    Debug.WriteLine(parser.Current);
                    var terms = parser.GetTerms();
                    if (terms[0] == "")
                    {
                        var description = terms[4];
                        if (description != string.Empty)
                        {
                            var names = description.Split(new char[] { '(', ')' },
                                StringSplitOptions.TrimEntries);
                            var sectionEntryNames = names[1].Split(":");
                            string mangledEntryName = (sectionEntryNames.Length > 1) ?
                                sectionEntryNames[1] :
                                $"_noname{_nonameCounter++}_";

                            string libraryName;
                            string moduleName;

                            var libraryModuleNames = names[0].Split(':', StringSplitOptions.TrimEntries);
                            if (libraryModuleNames.Length == 2)
                            {
                                libraryName = libraryModuleNames[0];
                                moduleName = libraryModuleNames[1];
                            }
                            else
                            {       
                                libraryName = string.Empty;
                                moduleName = libraryModuleNames[0];
                            }

                            var entryName = CdtUtility.DemangleTiName(mangledEntryName);
                            var entry = GetEntry(moduleName, entryName);
                            entry.Origin = uint.Parse(terms[2], NumberStyles.HexNumber);
                            entry.Length = uint.Parse(terms[3], NumberStyles.HexNumber);
                            entry.Library = GetLibrary(libraryName);
                            entry.Section = GetSection(sectionEntryNames[0]);
                        }
                    }
                }

                parser.MoveNext();
            }
        }
    }
}
