using BitStreams;
using System;
using System.Diagnostics;
using Utils.Helpers.Reflection;

namespace ResourceTypes.Prefab.CrashObject
{
    [PropertyClassAllowReflection]
    public class S_InitDeformPartCommon
    {
        public int[] Unk1 { get; set; } // 6 Floats, could be two Vec3s?
        public int[] Unk2 { get; set; } // Dynamic array of floats
        public byte Unk3 { get; set; } // flag to acknowledge extra data
        public uint Unk4 { get; set; } // count of unknown data.
        public byte Unk5 { get; set; } // flag to acknowledge extra data
        public byte Unk6 { get; set; } // flag to acknowledge extra data
        public int Unk7 { get; set; } // float
        public uint Unk8 { get; set; } // count of unknown data
        public S_InitDeformPartEffects PartEffects { get; set; }

        private int Nested_Unk01; // float
        private int Nested_Unk02; // float
        private int Nested_Unk03; // float
        private uint Nested_Unk04; // int
        private int Nested_Unk05; // float

        public void Load(BitStream MemStream)
        {
            // 6 floats - could be two Vec3s
            Unk1 = new int[6];
            for(int i = 0; i < Unk1.Length; i++)
            {
                Unk1[i] = MemStream.ReadInt32();
            }

            // Count - list of floats
            uint Unk2Count = MemStream.ReadUInt32();
            Unk2 = new int[Unk2Count];
            for (int i = 0; i < Unk2.Length; i++)
            {
                Unk2[i] = MemStream.ReadInt32();
            }

            Unk3 = MemStream.ReadBit(); // flag for extra data
            Debug.Assert(Unk3 == 0, "We expect one here. This has extra data!");

            Unk4 = MemStream.ReadUInt32(); // count of unknown data
            Debug.Assert(Unk4 == 0, "We expect one here. This has extra data!");

            Unk5 = MemStream.ReadBit(); // flag for extra data
            if(Unk5 == 1)
            {
                // in M2DE exe - sub_1402D1CF0
                // in logs - INITCOLLVOLUMENESTED
                Nested_Unk01 = MemStream.ReadInt32(); // float
                Nested_Unk02 = MemStream.ReadInt32(); // float
                Nested_Unk03 = MemStream.ReadInt32(); // float
                Nested_Unk04 = MemStream.ReadUInt32(); // int
                Nested_Unk05 = MemStream.ReadInt32(); // float
            }

            Unk6 = MemStream.ReadBit(); // flag for extra data
            Debug.Assert(Unk6 == 0, "We expect one here. This has extra data!");

            Unk7 = MemStream.ReadInt32();

            Unk8 = MemStream.ReadUInt32(); // count of unknown data
            Debug.Assert(Unk8 == 0, "We expect one here. This has extra data!");

            PartEffects = new S_InitDeformPartEffects();
            PartEffects.Load(MemStream);
        }

        public void Save(BitStream MemStream)
        {
            foreach (int Value in Unk1)
            {
                MemStream.WriteInt32(Value);
            }

            MemStream.WriteUInt32((uint)Unk2.Length);
            foreach (int Value in Unk2)
            {
                MemStream.WriteInt32(Value);
            }

            MemStream.WriteBit(Unk3);
            MemStream.WriteUInt32(Unk4);
            MemStream.WriteBit(Unk5);

            if(Unk5 == 1)
            {
                MemStream.WriteInt32(Nested_Unk01);
                MemStream.WriteInt32(Nested_Unk02);
                MemStream.WriteInt32(Nested_Unk03);
                MemStream.WriteUInt32(Nested_Unk04);
                MemStream.WriteInt32(Nested_Unk05);
            }

            MemStream.WriteBit(Unk6);
            MemStream.WriteInt32(Unk7);
            MemStream.WriteUInt32(Unk8);

            PartEffects.Save(MemStream);
        }
    }
}
