using BitStreams;

namespace ResourceTypes.Prefab.Vehicle
{
    public class C_Vector3
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public void Load(BitStream MemStream)
        {
            X = MemStream.ReadInt32();
            Y = MemStream.ReadInt32();
            Z = MemStream.ReadInt32();
        }

        public static C_Vector3 Construct(BitStream MemStream)
        {
            C_Vector3 Vector = new C_Vector3();
            Vector.Load(MemStream);
            return Vector;
        }
    }

    public class S_InitSeat
    {
        public uint Unk0 { get; set; } // m_Flags
        public ulong Unk1 { get; set; } // m_DoorIndexFrameName
        public C_Vector3 Unk2 { get; set; } // m_TargetName
        public C_Vector3 Unk3 { get; set; } // m_TargetSeat
        public C_Vector3 Unk4 { get; set; } // m_LockPos
        public C_Vector3 Unk5 { get; set; } // m_Dir
        public C_Vector3 Unk6 { get; set; } // m_Pos
        public uint Unk7 { get; set; } // m_SeatType
        public uint Unk8 { get; set; } // m_SeatIndex
        public uint Unk9 { get; set; } // m_SeatGroup
        public ulong Unk10 { get; set; } // m_FrameName

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt32();
            Unk1 = MemStream.ReadUInt64();
            Unk2 = C_Vector3.Construct(MemStream);
            Unk3 = C_Vector3.Construct(MemStream);
            Unk4 = C_Vector3.Construct(MemStream);
            Unk5 = C_Vector3.Construct(MemStream);
            Unk6 = C_Vector3.Construct(MemStream);
            Unk7 = MemStream.ReadUInt32();
            Unk8 = MemStream.ReadUInt32();
            Unk9 = MemStream.ReadUInt32();
            Unk10 = MemStream.ReadUInt64();
        }
    }
}
