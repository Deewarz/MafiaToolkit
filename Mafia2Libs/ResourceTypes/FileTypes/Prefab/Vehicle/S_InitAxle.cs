using BitStreams;

namespace ResourceTypes.Prefab.Vehicle
{
    public class S_InitAxle
    {
        public ulong AxleName { get; set; }
        public ulong BrakeDrumName { get; set; }
        public ulong RotWingName { get; set; }
        public uint AxleType { get; set; }
        public S_InitWheel Wheel { get; set; }

        public void Load(BitStream MemStream)
        {
            AxleName = MemStream.ReadUInt64();
            BrakeDrumName = MemStream.ReadUInt64();
            RotWingName = MemStream.ReadUInt64();
            AxleType = MemStream.ReadUInt32();

            Wheel = new S_InitWheel();
            Wheel.Load(MemStream);
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt64(AxleName);
            MemStream.WriteUInt64(BrakeDrumName);
            MemStream.WriteUInt64(RotWingName);
            MemStream.WriteUInt32(AxleType);

            Wheel.Save(MemStream);
        }
    }
}
