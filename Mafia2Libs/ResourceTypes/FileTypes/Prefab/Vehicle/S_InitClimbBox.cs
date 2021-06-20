using BitStreams;

namespace ResourceTypes.Prefab.Vehicle
{
    public class S_InitClimbBox
    {
        public C_Vector3 Unk0 { get; set; } // m_BoxMin
        public C_Vector3 Unk1 { get; set; } // m_BoxMax
        public ulong Unk2 { get; set; } // m_BoneFrameName
        public ulong Unk3 { get; set; } // m_DummyFrameName

        public void Load(BitStream MemStream)
        {
            Unk0 = C_Vector3.Construct(MemStream);
            Unk1 = C_Vector3.Construct(MemStream);
            Unk2 = MemStream.ReadUInt64();
            Unk3 = MemStream.ReadUInt64();
        }
    }
}
