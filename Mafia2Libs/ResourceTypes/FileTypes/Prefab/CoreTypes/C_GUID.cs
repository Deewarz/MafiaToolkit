using BitStreams;

namespace ResourceTypes.Prefab.Vehicle
{
    public class C_GUID
    {
        public uint Part0 { get; set; }
        public uint Part1 { get; set; }

        public void Load(BitStream MemStream)
        {
            Part0 = MemStream.ReadUInt32();
            Part1 = MemStream.ReadUInt32();
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt32(Part0);
            MemStream.WriteUInt32(Part1);
        }
    }
}
