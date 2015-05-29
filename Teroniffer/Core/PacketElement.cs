using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detrav.Teroniffer.Core
{
    class PacketElement
    {
        public static string[] types = new string[] 
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
        public ushort start = 0;
        public ushort end = 0;
        public string type = null;
        public object value = null;

        public PacketElement() { }
        public PacketElement(string name, ushort start, string type, object value, ushort end = 0)
        {
            this.name = name;
            this.start = start;
            this.end = end;
            this.type = type;
            this.value = value;
        }

        public PacketElement(PacketElement el, string value = null)
        {
            name = el.name;
            start = el.start;
            end = el.end;
            type = el.type;
            this.value = value;
        }
    }
}
