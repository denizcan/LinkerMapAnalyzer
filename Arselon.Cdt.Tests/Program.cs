using Arselon.Cdt.Extractor;
using Arselon.Cdt.Models;
using System;
using System.IO;

namespace Arselon.Cdt.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Map file path required");
                return;
            }
            var lines = File.ReadAllLines(args[0]);
            var extractor = new TiExtractor();
            extractor.ExtractSectionAllocationMap(lines);
        }
    }
}
