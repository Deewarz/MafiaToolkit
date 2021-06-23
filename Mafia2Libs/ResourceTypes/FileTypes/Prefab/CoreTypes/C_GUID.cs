using BitStreams;
using Utils.Helpers.Reflection;

namespace ResourceTypes.Prefab.Vehicle
{
    [PropertyClassAllowReflection]
    public class C_GUID
    {
        [PropertyForceAsAttribute]
        public uint Part0 { get; set; }
        [PropertyForceAsAttribute]
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
