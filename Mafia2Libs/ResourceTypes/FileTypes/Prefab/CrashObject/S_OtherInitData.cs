using BitStreams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_OtherInitData
    {
        public class Type0
        {
            public ulong Hash { get; set; }
            public int Unk0 { get; set; }
            public int Unk1 { get; set; }
        }

        public class Type1
        {
            public ulong Hash { get; set; }
            public ulong[] OtherHashes { get; set; }
            public int Unk1 { get; set; } // Could be float
            public byte Unk2 { get; set; } // Could be if

            public void Load(BitStream MemStream)
            {
                Hash = MemStream.ReadUInt64();

                uint NumHashes = MemStream.ReadUInt32();
                OtherHashes = new ulong[NumHashes];
                for(uint i = 0; i < NumHashes; i++)
                {
                    OtherHashes[i] = MemStream.ReadUInt64();
                }

                Unk1 = MemStream.ReadInt32();
                Unk2 = MemStream.ReadBit();
                if(Unk2 > 0)
                {
                    int z = 0;
                }
            }
        }

        public void Load(BitStream MemStream)
        {
            ulong Unk0 = MemStream.ReadUInt64();
            ulong Unk1 = MemStream.ReadUInt64();
            ulong Unk2 = MemStream.ReadUInt64();

            uint NumHashes = MemStream.ReadUInt32();
            ulong[] Hashes0 = new ulong[NumHashes];
            for (uint i = 0; i < NumHashes; i++)
            {
                Hashes0[i] = MemStream.ReadUInt64();
            }

            NumHashes = MemStream.ReadUInt32();
            ulong[] Hashes1 = new ulong[NumHashes];
            for (uint i = 0; i < NumHashes; i++)
            {
                Hashes1[i] = MemStream.ReadUInt64();
            }

            NumHashes = MemStream.ReadUInt32();
            ulong[] Hashes2 = new ulong[NumHashes];
            for (uint i = 0; i < NumHashes; i++)
            {
                Hashes2[i] = MemStream.ReadUInt64();
            }

            NumHashes = MemStream.ReadUInt32();
            ulong[] Hashes3 = new ulong[NumHashes];
            for (uint i = 0; i < NumHashes; i++)
            {
                Hashes3[i] = MemStream.ReadUInt64();
            }

            NumHashes = MemStream.ReadUInt32();
            ulong[] Hashes4 = new ulong[NumHashes];
            for (uint i = 0; i < NumHashes; i++)
            {
                Hashes4[i] = MemStream.ReadUInt64();
            }

            NumHashes = MemStream.ReadUInt32();
            if(NumHashes > 0)
            {
                int z = 0;
            }

            int Unk3 = MemStream.ReadInt32(); // float
            int Unk5 = MemStream.ReadInt32(); // float
            int Unk6 = MemStream.ReadInt32(); // float
            int Unk7 = MemStream.ReadInt32(); // float

            // Matrices
            int[] Matrix = new int[12];
            for(uint i = 0; i < 12; i++)
            {
                Matrix[i] = MemStream.ReadInt32(); // float
            }

            int[] Matrix1 = new int[12];
            for (uint i = 0; i < 12; i++)
            {
                Matrix1[i] = MemStream.ReadInt32(); // float
            }

            int[] Matrix2 = new int[12];
            for (uint i = 0; i < 12; i++)
            {
                Matrix2[i] = MemStream.ReadInt32(); // float
            }

            uint NumType0s = MemStream.ReadUInt32();
            Type0[] Data = new Type0[NumType0s];
            for(uint i = 0; i < NumType0s; i++)
            {
                Type0 NewData = new Type0();
                NewData.Hash = MemStream.ReadUInt64();
                NewData.Unk0 = MemStream.ReadInt32();
                NewData.Unk1 = MemStream.ReadInt32();
                Data[i] = NewData;
            }

            uint NumType1s = MemStream.ReadUInt32();
            Type1[] Data1 = new Type1[NumType1s];
            for (uint i = 0; i < NumType1s; i++)
            {
                Type1 NewData = new Type1();
                NewData.Load(MemStream);
                Data1[i] = NewData;
            }

            ulong Unk10 = MemStream.ReadUInt64();
            ulong Unk11 = MemStream.ReadUInt64();
            ulong Unk12 = MemStream.ReadUInt64();
            ulong Unk13 = MemStream.ReadUInt64();
        }
    }
}
