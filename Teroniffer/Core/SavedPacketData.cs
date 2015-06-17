using Detrav.TeraApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detrav.Teroniffer.Core
{
    class SavedPacketData
    {
        public PacketType type { get; set; }
        public byte[] data { get; set; }
        public DateTime time { get; set; }

        public SavedPacketData(PacketType type, byte[] data,DateTime time)
        {
            this.type = type;
            this.data = data;
            this.time = time;
        }

        public SavedPacketData() : this(PacketType.Any, null, DateTime.MinValue) { }
    }
}
