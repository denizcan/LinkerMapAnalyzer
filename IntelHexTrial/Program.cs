using Arselon.Cdt.Hex;

namespace IntelHexTrial
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BinaryMap map = new BinaryMap();    
            IntelHex.ParseFile(args[0], map);
        }
    }
}
