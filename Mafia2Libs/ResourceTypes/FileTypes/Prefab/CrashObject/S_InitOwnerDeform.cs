using BitStreams;
using System;
using System.Diagnostics;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_InitOwnerDeform
    {
        public class DataPacket
        {
            public ulong Unk0 { get; set; }
            public int[] Unk1 { get; set; } // transform?

            public void Load(BitStream MemStream)
            {
                Unk0 = MemStream.ReadUInt64();

                // BitStream type of something
                // I think its transform (floats)
                Unk1 = new int[12];
                for (int i = 0; i < Unk1.Length; i++)
                {
                    Unk1[i] = MemStream.ReadInt32();
                }
            }
        }

        public ulong Unk0 { get; set; }
        public ulong Unk1 { get; set; }
        public int[] Unk2 { get; set; } // transform?
        public int[] Unk3 { get; set; } // Two Vec3s?
        public ushort[] Unk4 { get; set; } // array of index?
        public ushort Unk5 { get; set; }
        public uint Unk6 { get; set; }
        public ushort Unk7 { get; set; }
        public uint Unk8 { get; set; }
        public DataPacket[] Unk9 { get; set; }
        public int[] Unk10 { get; set; } // transform?

        public void Load(BitStream MemStream)
        {
            // two hashes (is 2nd one is usually empty?)
            Unk0 = MemStream.ReadUInt64();
            Unk1 = MemStream.ReadUInt64();

            // BitStream type of something
            // I think its transform (floats)
            Unk2 = new int[12];
            for (int i = 0; i < Unk2.Length; i++)
            {
                Unk2[i] = MemStream.ReadInt32();
            }

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
            Unk8 = MemStream.ReadUInt32();

            Unk9 = new DataPacket[Unk8];
            for(int i = 0; i < Unk9.Length; i++)
            {
                DataPacket NewPacket = new DataPacket();
                NewPacket.Load(MemStream);
                Unk9[i] = NewPacket;
            }

            // BitStream type of something
            // I think its transform (floats)
            Unk10 = new int[12];
            for (int i = 0; i < Unk10.Length; i++)
            {
                Unk10[i] = MemStream.ReadInt32();
            }
        }
    }
}
