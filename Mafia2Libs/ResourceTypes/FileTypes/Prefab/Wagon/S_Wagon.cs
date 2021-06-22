using BitStreams;
using ResourceTypes.Prefab.CrashObject;

namespace ResourceTypes.Prefab.Wagon
{
    public class S_Wagon : S_DeformationInitData
    {
        public uint[] Data { get; set; }

        public override void Load(BitStream MemStream)
        {
            base.Load(MemStream);

            // Not present in any game
            uint NumData = MemStream.ReadUInt32();
            Data = new uint[NumData];
        }

        public override void Save(BitStream MemStream)
        {
            base.Save(MemStream);

            // Not present in any game
            MemStream.WriteUInt32((uint)Data.Length);
        }
    }
}
