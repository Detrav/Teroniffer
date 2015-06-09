using Detrav.TeraApi.Interfaces;
using Detrav.TeraApi.OpCodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detrav.Teroniffer.Core
{
    class PacketStructureManager
    {
        public static IAssetManager assets;
        private static Dictionary<ushort, PacketStructure> packetStructures = new Dictionary<ushort, PacketStructure>();


        public static PacketStructure loadStructure(object opCode)
        {
            string version = PacketCreator.getCurrentVersion().ToString();
            string fileName = opCode.ToString() + ".json";
            string file = Path.Combine(version, fileName);
            //Тут меняем, сначало пытаемся загнрузить внешний файл
            PacketStructure ps = (PacketStructure)assets.deSerialize(file, typeof(PacketStructure));
            if (ps == null)
            { 
                //Пытаемся загрузить внутрений файл
                ps = (PacketStructure)assets.deSerialize(Path.Combine("assets",file), typeof(PacketStructure), AssetType.local);
                if (ps == null) return null;
            }
            packetStructures[(ushort)opCode] = ps;
            return ps;
        }

        public static void saveStructure(object opCode,PacketStructure packetStructure)
        {
            string version = PacketCreator.getCurrentVersion().ToString();
            string fileName = opCode.ToString() + ".json";
            string file = Path.Combine(version, fileName);
            packetStructures[(ushort)opCode] = packetStructure;
            assets.serialize(file, packetStructure);
        }

        public static PacketStructure getStructure(object opCode)
        {
            PacketStructure ps;
            if (packetStructures.TryGetValue((ushort)opCode, out ps))
                return ps;
            ps = loadStructure(opCode);
            if (ps == null)
            {
                ps = new PacketStructure(true);
                packetStructures.Add((ushort)opCode, ps);
            }
            return ps;
        }
    }
}
