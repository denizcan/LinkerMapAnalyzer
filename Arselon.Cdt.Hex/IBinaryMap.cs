using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arselon.Cdt.Hex
{
    public interface IBinaryMap
    {
        void AddData(uint address, byte[] data);
    }
}
