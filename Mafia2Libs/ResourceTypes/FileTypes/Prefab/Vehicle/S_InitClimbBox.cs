using BitStreams;

namespace ResourceTypes.Prefab.Vehicle
{
    public class S_InitClimbBox
    {
        public C_Vector3 BoxMin { get; set; }
        public C_Vector3 BoxMax { get; set; }
        public ulong BoneFrameName { get; set; }
        public ulong DummyFrameName { get; set; }

        public void Load(BitStream MemStream)
        {
            BoxMin = C_Vector3.Construct(MemStream);
            BoxMax = C_Vector3.Construct(MemStream);
            BoneFrameName = MemStream.ReadUInt64();
            DummyFrameName = MemStream.ReadUInt64();
        }

        public void Save(BitStream MemStream)
        {
            BoxMin.Save(MemStream);
            BoxMax.Save(MemStream);
            MemStream.WriteUInt64(BoneFrameName);
            MemStream.WriteUInt64(DummyFrameName);
        }
    }
}
