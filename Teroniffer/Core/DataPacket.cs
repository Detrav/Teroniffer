using Detrav.TeraApi;
using Detrav.TeraApi.OpCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detrav.Teroniffer.Core
{
    class DataPacket
    {
        public int num { get; set; }
        public PacketType type { get; set; }
        public ushort size { get; set; }
        public object opCode { get; set; }
        public DateTime time { get; set; }
        private TeraPacketWithData packet;
        public DataPacket(int num, TeraPacketWithData packet)
        {
            this.packet = packet;
            this.num = num;
            this.type = packet.type;
            this.size = packet.size;
            this.time = packet.time;
            this.opCode = PacketCreator.getOpCode(packet.opCode);
        }

        public TeraPacketWithData getTeraPacket() { return packet; }
    }
}
