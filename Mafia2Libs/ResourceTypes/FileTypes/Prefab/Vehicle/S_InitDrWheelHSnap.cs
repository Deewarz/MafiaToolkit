using BitStreams;

namespace ResourceTypes.Prefab.Vehicle
{
    public class S_InitDrWheelHSnap
    {
        public ulong Unk0 { get; set; } // m_DrWheelFrameName
        public ulong Unk1 { get; set; } // m_LeftSnapFrameName
        public ulong Unk2 { get; set; } // m_RightSnapFrameName

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt64();
            Unk1 = MemStream.ReadUInt64();
            Unk2 = MemStream.ReadUInt64();
        }
    }
}
