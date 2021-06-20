using BitStreams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_InitAxle
    {
        public ulong AxleName { get; set; }
        public ulong BrakeDrumName { get; set; }
        public ulong RotWingName { get; set; }
        public uint AxleType { get; set; }
        public S_InitWheel Wheel { get; set; }

        public void Load(BitStream MemStream)
        {
            AxleName = MemStream.ReadUInt64();
            BrakeDrumName = MemStream.ReadUInt64();
            RotWingName = MemStream.ReadUInt64();
            AxleType = MemStream.ReadUInt32();

            Wheel = new S_InitWheel();
            Wheel.Load(MemStream);
        }
    }
}
