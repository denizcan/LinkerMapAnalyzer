using System.Data;

namespace Arselon.Cdt.Hex
{
    public class IntelHex
    {
        public static void Parse(TextReader textReader, IBinaryMap map)
        {
            long baseAddress = 0;
            var hexReader = new HexReader(textReader);
            while (true)
            {
                hexReader.ReadUntil(':');
                var linenumber = hexReader.LineNumber;
                var columnNumber = hexReader.ColumnNumber;

                var byteCount = hexReader.ReadByte();
                var address = hexReader.ReadUInt16();
                var recordType = hexReader.ReadByte();
                var data = hexReader.ReadBytes(byteCount);
                var crc = hexReader.ReadByte();
                
                var crcCalculated = (int)byteCount + (address >> 8) + (address & 0xff) + recordType;
                foreach (var b in data)
                    crcCalculated += b;
                crcCalculated = ((crcCalculated ^ 0xFF) + 1) & 0xFF;
                if (crc != crcCalculated)
                    throw new InvalidDataException($"CRC does not match, expected {crcCalculated}, read {crc}");
                hexReader.ReadEol();

                switch (recordType)
                {
                    case 0:
                        // data
                        map.AddData(baseAddress + address, data);
                        break;

                    case 1:
                        // eof
                        return;

                    case 2:
                        // extended segment address
                        if (byteCount != 2)
                            throw new InvalidDataException($"Invalid address record at {linenumber}:{columnNumber}");
                        baseAddress = BitConverter.ToInt16(data, 0) * 16;
                        break;
                }
            }
        }

        public static void ParseFile(string fileName, IBinaryMap map)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                var textReader = new StreamReader(fileStream);
                Parse(textReader, map);
            }
        }
    }
}
