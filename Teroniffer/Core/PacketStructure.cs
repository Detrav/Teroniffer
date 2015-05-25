using Detrav.TeraApi;
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
        public PacketStructure()
        {
            elements.Add(new PacketElement() { name = "size", start = "0", type = "ushort" });
            elements.Add(new PacketElement() { name = "opCode", start = "2", type = "ushort" });
        }
        public string parse(TeraPacketWithData packet)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Offset 00 01 02 03 04 05 06 07 | 08 09 0A 0B 0C 0D 0E 0F  0123456789ABCDEF\n");
            for (int i = 0; i < packet.data.Length; i += 16)
            {
                sb.AppendFormat(" {0:X4}: {1,-24}| {2,-24} {3,-16}\n", i, packet.toHex(i, i+ 8," "), packet.toHex(i + 8, i+ 16," "), packet.toSingleString(i,i+16));
            }
            sb.Append("\n");
            foreach (var el in elements)
            {
                string val = getElementValue(packet, el).ToString();
                sb.AppendFormat("{0:X4} - {1} : {2} : {3}\n", el.start, el.name, val, el.type);
            }
            return sb.ToString();

        }

        private ushort getElementEnd(TeraPacketWithData packet, PacketElement el)
        {
            if(el.end == null) return 0;
            ushort result;
            if(UInt16.TryParse(el.end,out result)) return result;
            return (ushort)getElementValue(packet, el.end);
        }

        private object getElementValue(TeraPacketWithData packet, string p)
        {
            foreach (var el in elements)
            {
                if (el.name == p)
                    return getElementValue(packet, el);
            }
            return null;
        }

        public object getElementValue(TeraPacketWithData packet, PacketElement el)
        {
            ushort start = getElementStart(packet, el);
            ushort end = getElementEnd(packet, el);
            switch(el.type)
            {
                case "byte": return packet.toByte(start);
                case "sbyte": return packet.toSByte(start);
                case "ushort": return packet.toUInt16(start);
                case "short": return packet.toInt16(start);
                case "uint": return packet.toUInt32(start);
                case "int": return packet.toInt32(start);
                case "ulong": return packet.toUInt64(start);
                case "long": return packet.toInt64(start);
                case "float": return packet.toSingle(start);
                case "double": return packet.toDouble(start);
                case "singleChar": return packet.toSingleChar(start);
                case "doubleChar": return packet.toDoubleChar(start);
                case "singleString": return packet.toSingleString(start, end);
                case "doubleString": return packet.toDoubleString(start, end);
                case "bool": return packet.toBoolean(start);
                case "hex": return packet.toHex(start, end);
                default: return "[UNKNOWN]";
            }
        }

        private ushort getElementStart(TeraPacketWithData packet, PacketElement el)
        {
            if (el.start == null) return 0;
            ushort result;
            if (UInt16.TryParse(el.start, out result)) return result;
            return (ushort)getElementValue(packet, el.start);
        }

        

        

       
    }
}
