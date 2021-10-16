using BitStreams;
using System;
using System.ComponentModel;
using System.Diagnostics;
using Utils.Helpers.Reflection;

namespace ResourceTypes.Prefab.CrashObject
{
    [TypeConverter(typeof(ExpandableObjectConverter)), PropertyClassAllowReflection]
    public class S_InitDeformPartCommon
    {
        public int[] Unk1 { get; set; } // 6 Floats, could be two Vec3s?
        public int[] Unk2 { get; set; } // Dynamic array of floats
        public byte Unk3 { get; set; } // flag to acknowledge extra data
        public C_Transform Unk3_Transform { get; set; }
        public uint Unk4 { get; set; } // count of unknown data.
        public byte Unk5 { get; set; } // flag to acknowledge extra data
        public S_InitCollVolume_Nested Unk5_Data { get; set; } // NOT ACTUALLY COLLISION DATA..
        public byte Unk6 { get; set; } // flag to acknowledge extra data
        public string Unk6_Value { get; set; }
        public int Unk7 { get; set; } // float
        public uint[] Unk8 { get; set; } // unknown data
        public S_InitDeformPartEffects PartEffects { get; set; }

        public S_InitDeformPartCommon()
        {
            Unk1 = new int[0];
            Unk2 = new int[0];
            Unk3_Transform = new C_Transform();
            Unk6_Value = String.Empty;
            Unk8 = new uint[0];
            PartEffects = new S_InitDeformPartEffects();
        }

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
            if(Unk3 == 1)
            {
                Unk3_Transform = new C_Transform();
                Unk3_Transform.Load(MemStream);
            }

            Unk4 = MemStream.ReadUInt32(); // count of unknown data
            Debug.Assert(Unk4 == 0, "We expect one here. This has extra data!");

            Unk5 = MemStream.ReadBit(); // flag for extra data
            if(Unk5 == 1)
            {
                // in M2DE exe - sub_1402D1CF0
                // in logs - INITCOLLVOLUMENESTED
                Unk5_Data = new S_InitCollVolume_Nested();
                Unk5_Data.Load(MemStream);
            }

            Unk6 = MemStream.ReadBit(); // flag for extra data
            if(Unk6 == 1)
            {
                Unk6_Value = MemStream.ReadString32();
            }

            Unk7 = MemStream.ReadInt32();

            // Unknown data. Could be indexes?
            uint NumUnk8 = MemStream.ReadUInt32();
            Unk8 = new uint[NumUnk8];
            for(uint i = 0; i < NumUnk8; i++)
            {
                Unk8[i] = MemStream.ReadUInt32();
            }

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

            // Unknown transform
            MemStream.WriteBit(Unk3);
            if(Unk3 == 1)
            {
                Unk3_Transform.Save(MemStream);
            }

            MemStream.WriteUInt32(Unk4);

            // Unknown data
            MemStream.WriteBit(Unk5);
            if(Unk5 == 1)
            {
                Unk5_Data.Save(MemStream);
            }

            MemStream.WriteBit(Unk6);
            if(Unk6 == 1)
            {
                MemStream.WriteString32(Unk6_Value);
            }

            MemStream.WriteInt32(Unk7);

            // Unknown data. Could be indexes?
            MemStream.WriteUInt32((uint)Unk8.Length);
            foreach(uint Value in Unk8)
            {
                MemStream.WriteUInt32(Value);
            }

            PartEffects.Save(MemStream);
        }
    }
}
