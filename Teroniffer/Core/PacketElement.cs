using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detrav.Teroniffer.Core
{
    class PacketElement
    {
        static string[] types = new string[] 
        {
            "byte",
            "sbyte",
            "ushort",
            "short",
            "uint",
            "int",
            "ulong",
            "long",
            "float",
            "double",
            "singleChar",
            "doubleChar",
            "singleString",
            "doubleString",
            "bool",
            "hex" 
        };
        public string name = null;
        public string start = null;
        public string end = null;
        public string type = null;
    }
}
