using BitStreams;

namespace ResourceTypes.Prefab.Vehicle
{
    public class S_InitDCBData
    {
        public ulong DoorFrameName { get; set; }
        public float Resistance { get; set; }
        public float Hitpoints { get; set; }

        public void Load(BitStream MemStream)
        {
            DoorFrameName = MemStream.ReadUInt64();
            Resistance = MemStream.ReadSingle();
            Hitpoints = MemStream.ReadSingle();
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt64(DoorFrameName);
            MemStream.WriteSingle(Resistance);
            MemStream.WriteSingle(Hitpoints);
        }
    }
}
