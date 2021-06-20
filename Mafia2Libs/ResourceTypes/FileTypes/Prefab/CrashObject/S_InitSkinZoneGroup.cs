using BitStreams;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_InitSkinZoneGroup
    {
        public ushort Unk0 { get; set; } // m_MaterialGroup
        public ushort Unk1 { get; set; } // m_SkinZoneRange
        public ushort Unk2 { get; set; } // m_SkinZoneGroupIndex

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt16();
            Unk1 = MemStream.ReadUInt16();
            Unk2 = MemStream.ReadUInt16();
        }
    }
}
