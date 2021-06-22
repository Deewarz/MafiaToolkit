using BitStreams;
using System.Diagnostics;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_ActorDeformInitData : S_DeformationInitData
    {
        public uint Unk0 { get; set; } // Could be count
        public uint Unk1 { get; set; } // Could be another Count

        public override void Load(BitStream MemStream)
        {
            base.Load(MemStream);

            Unk0 = MemStream.ReadUInt32();
            Debug.Assert(Unk0 == 0, "Extra data detected");

            Unk1 = MemStream.ReadUInt32();
            Debug.Assert(Unk0 == 0, "Extra data detected");
        }

        public override void Save(BitStream MemStream)
        {
            base.Save(MemStream);

            MemStream.WriteUInt32(Unk0);
            MemStream.WriteUInt32(Unk1);
        }
    }
}
