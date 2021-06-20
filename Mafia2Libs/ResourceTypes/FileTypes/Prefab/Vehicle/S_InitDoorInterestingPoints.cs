using BitStreams;

namespace ResourceTypes.Prefab.Vehicle
{
    public class S_InitDoorInterestingPoints
    {
        public C_Vector3 Unk0 { get; set; } // m_CarDoorHandlePos
        public C_Vector3 Unk1 { get; set; } // m_CarDoorLockPos
        public ulong Unk2 { get; set; } // m_DoorFrameName

        public void Load(BitStream MemStream)
        {
            Unk0 = C_Vector3.Construct(MemStream);
            Unk1 = C_Vector3.Construct(MemStream);
            Unk2 = MemStream.ReadUInt64();
        }
    }
}
