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
        public static void loadStructures()
        {
            string version = PacketCreator.getCurrentVersion().ToString();
            var files = assets.getFiles(version, "*.json");
            foreach(var file in files)
            {
                object f = PacketCreator.parseOpCode(Path.GetFileNameWithoutExtension(file));
                if (f != null)
                    loadStructure(f);
            }
        }

        public static PacketStructure loadStructure(object opCode)
        {
            string version = PacketCreator.getCurrentVersion().ToString();
            string fileName = opCode.ToString() + ".json";
            string file = Path.Combine(version, fileName);
            PacketStructure ps = (PacketStructure)assets.deSerialize(file, typeof(PacketStructure));
            if (ps == null) return null;
            if (packetStructures.Keys.Contains((ushort)opCode))
                packetStructures.Remove((ushort)opCode);
            packetStructures.Add((ushort)opCode, ps);
            return ps;
        }

        public static void saveStructures()
        {
            foreach(var pair in packetStructures)
            {
                saveStructure(PacketCreator.getOpCode(pair.Key), pair.Value);
            }
        }

        public static void saveStructure(object opCode,PacketStructure packetStructure)
        {
            string version = PacketCreator.getCurrentVersion().ToString();
            string fileName = opCode.ToString() + ".json";
            string file = Path.Combine(version, fileName);
            assets.serialize(file, packetStructure);
        }

        public static void setStructure(object opCode, PacketStructure packetStructure)
        {
            PacketStructure ps;
            if (packetStructures.TryGetValue((ushort)opCode, out ps))
                packetStructures[(ushort)opCode] = packetStructure;
            else
                packetStructures.Add((ushort)opCode, packetStructure);
            saveStructure(opCode, packetStructure);
        }

        public static PacketStructure getStructure(object opCode)
        {
            PacketStructure ps;
            if (packetStructures.TryGetValue((ushort)opCode, out ps))
                return ps;
            ps = loadStructure(opCode);
            if(ps==null) return new PacketStructure(true);
            return ps;
        }
    }
}
