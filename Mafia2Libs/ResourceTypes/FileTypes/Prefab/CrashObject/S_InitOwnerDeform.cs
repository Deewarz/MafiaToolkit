using BitStreams;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_InitOwnerDeform
    {
        public class DataPacket
        {
            public ulong Unk0 { get; set; }
            public C_Transform Unk1 { get; set; } // transform?

            public DataPacket()
            {
                Unk1 = new C_Transform();
            }

            public void Load(BitStream MemStream)
            {
                Unk0 = MemStream.ReadUInt64();
                Unk1.Load(MemStream);
            }

            public void Save(BitStream MemStream)
            {
                MemStream.WriteUInt64(Unk0);
                Unk1.Save(MemStream);
            }
        }

        public ulong Unk0 { get; set; }
        public ulong Unk1 { get; set; }
        public C_Transform Unk2 { get; set; }
        public int[] Unk3 { get; set; } // Two Vec3s?
        public ushort[] Unk4 { get; set; } // array of index?
        public ushort Unk5 { get; set; }
        public uint Unk6 { get; set; }
        public ushort Unk7 { get; set; }
        public DataPacket[] Unk9 { get; set; }
        public C_Transform Unk10 { get; set; } // transform?

        public S_InitOwnerDeform()
        {
            Unk2 = new C_Transform();
            Unk3 = new int[0];
            Unk4 = new ushort[0];
            Unk9 = new DataPacket[0];
            Unk10 = new C_Transform();
        }

        public void Load(BitStream MemStream)
        {
            // two hashes (is 2nd one is usually empty?)
            Unk0 = MemStream.ReadUInt64();
            Unk1 = MemStream.ReadUInt64();

            // Read transform
            Unk2.Load(MemStream);

            // BitStream type of something
            // Two Vec3s?
            Unk3 = new int[6];
            for (int i = 0; i < Unk3.Length; i++)
            {
                Unk3[i] = MemStream.ReadInt32();
            }

            // Array of Indexes?
            uint Unk4Count = MemStream.ReadUInt32();
            Unk4 = new ushort[Unk4Count];
            for (int i = 0; i < Unk4.Length; i++)
            {
                Unk4[i] = MemStream.ReadUInt16();
            }

            Unk6 = MemStream.ReadUInt32();
            Unk7 = MemStream.ReadUInt16();

            // Read unknown data
            uint NumDataPackets = MemStream.ReadUInt32();
            Unk9 = new DataPacket[NumDataPackets];
            for(int i = 0; i < Unk9.Length; i++)
            {
                DataPacket NewPacket = new DataPacket();
                NewPacket.Load(MemStream);
                Unk9[i] = NewPacket;
            }

            // Read transform
            Unk10.Load(MemStream);
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt64(Unk0);
            MemStream.WriteUInt64(Unk1);

            // Write Transform
            Unk2.Save(MemStream);

            // Tow Vec3s?
            foreach (int Value in Unk3)
            {
                MemStream.WriteInt32(Value);
            }

            MemStream.WriteUInt32((uint)Unk4.Length);
            foreach (ushort Value in Unk4)
            {
                MemStream.WriteUInt16(Value);
            }

            MemStream.WriteUInt32(Unk6);
            MemStream.WriteUInt16(Unk7);

            // Write unknown data
            MemStream.WriteUInt32((uint)Unk9.Length);
            foreach (DataPacket Value in Unk9)
            {
                Value.Save(MemStream);
            }

            // Write Transform
            Unk10.Save(MemStream);
        }
    }
}
