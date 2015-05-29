using Detrav.TeraApi;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detrav.Teroniffer.Core
{
    class PacketStructure
    {
        List<PacketElement> elements = new List<PacketElement>();
        TeraPacketWithData packet;
        public string script;
        private Lua lua;

        public PacketStructure(bool _new)
        {
            lua = new Lua();
            lua.RegisterFunction("addElement", this, this.GetType().GetMethod("addElement"));
            if (_new)
            {
                script = @"";
                //elements.Add(new PacketElement() { name = "size", start = "0", type = "ushort" });
                //elements.Add(new PacketElement() { name = "opCode", start = "2", type = "ushort" });
            }
        }
        public PacketStructure() : this(false) { }


        public string parse(TeraPacketWithData packet)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Offset 00 01 02 03 04 05 06 07 | 08 09 0A 0B 0C 0D 0E 0F  0123456789ABCDEF\n");
            for (int i = 0; i < packet.data.Length; i += 16)
            {
                sb.AppendFormat(" {0:X4}: {1,-24}| {2,-24} {3,-16}\n", i, packet.toHex(i, i + 8, " "), packet.toHex(i + 8, i + 16, " "), packet.toSingleString(i, i + 16));
            }
            sb.Append("\n");
            //тут начало луа
            this.packet = packet;
            lua.DoString(script);
            this.packet = null;
            //тут конец
            foreach(var el in elements)
            {
                sb.AppendFormat("{0:X4} - {1} : {2} : {3}\n", el.start, el.name, el.value, el.type);
            }
            return sb.ToString();
        }

        public object addElement(string name, ushort start,string type, ushort end = 0)
        {
            object val;
            switch (type)
            {
                case "byte": val = packet.toByte(start); break;
                case "sbyte": val = packet.toSByte(start); break;
                case "ushort": val = packet.toUInt16(start); break;
                case "short": val = packet.toInt16(start); break;
                case "uint": val = packet.toUInt32(start); break;
                case "int": val = packet.toInt32(start); break;
                case "ulong": val = packet.toUInt64(start); break;
                case "long": val = packet.toInt64(start); break;
                case "float": val = packet.toSingle(start); break;
                case "double": val = packet.toDouble(start); break;
                case "singleChar": val = packet.toSingleChar(start); break;
                case "doubleChar": val = packet.toDoubleChar(start); break;
                case "singleString": val = packet.toSingleString(start, end); break;
                case "doubleString": val = packet.toDoubleString(start, end); break;
                case "bool": val = packet.toBoolean(start); break;
                case "hex": val = packet.toHex(start, end); break;
                default: val = "[UNKNOWN]"; break;
            }
            elements.Add(new PacketElement(name, start, type, val, end));
            return val;
        }
    }
}