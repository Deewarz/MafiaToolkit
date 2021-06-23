using BitStreams;

namespace ResourceTypes.Prefab.Vehicle
{
    public class S_InitWindowData
    {
        public ulong WindowFrameName { get; set; }
        public ulong[] CheckBoneFrameName { get; set; }
        public float Depth { get; set; }
        public bool bIsOpenable { get; set; }

        public S_InitWindowData()
        {
            CheckBoneFrameName = new ulong[0];
        }

        public void Load(BitStream MemStream)
        {
            WindowFrameName = MemStream.ReadUInt64();
            CheckBoneFrameName = PrefabUtils.ReadHashArray(MemStream);
            Depth = MemStream.ReadSingle();
            bIsOpenable = MemStream.ReadBit();
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt64(WindowFrameName);
            PrefabUtils.WriteHashArray(MemStream, CheckBoneFrameName);
            MemStream.WriteSingle(Depth);
            MemStream.WriteBit(bIsOpenable);
        }
    }
}
