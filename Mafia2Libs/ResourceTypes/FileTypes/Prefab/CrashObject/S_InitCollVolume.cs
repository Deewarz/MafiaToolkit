﻿using BitStreams;
using System;
using System.ComponentModel;
using System.Diagnostics;
using Utils.Helpers.Reflection;

namespace ResourceTypes.Prefab.CrashObject
{
    [TypeConverter(typeof(ExpandableObjectConverter)), PropertyClassAllowReflection]
    public class S_InitCollVolume_Nested
    {
        public float Unk0 { get; set; }
        public float Unk1 { get; set; }
        public float Unk2 { get; set; }
        public int Unk3 { get; set; }
        public float Unk4 { get; set; }

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadSingle();
            Unk1 = MemStream.ReadSingle();
            Unk2 = MemStream.ReadSingle();
            Unk3 = MemStream.ReadInt32();
            Unk4 = MemStream.ReadSingle();
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteSingle(Unk0);
            MemStream.WriteSingle(Unk1);
            MemStream.WriteSingle(Unk2);
            MemStream.WriteInt32(Unk3);
            MemStream.WriteSingle(Unk4);
        }
    }

    [PropertyClassAllowReflection]
    public class S_InitCollVolumeCollection
    {
        public S_InitCollVolume[] Volumes { get; set; }

        public S_InitCollVolumeCollection()
        {
            Volumes = new S_InitCollVolume[0];
        }

        public void Read(BitStream MemStream)
        {
            uint NumCollisionVolumes = MemStream.ReadUInt32(); // Count
            Volumes = new S_InitCollVolume[NumCollisionVolumes];
            for (int x = 0; x < NumCollisionVolumes; x++)
            {
                S_InitCollVolume CollisionVolume = new S_InitCollVolume();
                CollisionVolume.Load(MemStream);
                Volumes[x] = CollisionVolume;
            }
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt32((uint)Volumes.Length);
            foreach (S_InitCollVolume Value in Volumes)
            {
                Value.Save(MemStream);
            }
        }
    }

    [PropertyClassAllowReflection]
    public class S_InitCollVolume
    {
        public uint Unk0 { get; set; }
        public C_Transform Unk1 { get; set; } // transform?
        public byte Unk2 { get; set; } // if 1 - means something is available
        public int[] Unk3 { get; set; } // Vector3?
        public ulong[] Unk4 { get; set; } // hashes?
        public byte Unk5 { get; set; } // if 1 - means something is available
        public S_InitCollVolume_Nested Unk6 { get; set; }

        public S_InitCollVolume()
        {
            Unk1 = new C_Transform();
            Unk3 = new int[0];
            Unk4 = new ulong[0];
            Unk6 = new S_InitCollVolume_Nested();
        }

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt32();

            // BitStream type of something
            // I think its transform (floats)
            Unk1.Load(MemStream);

            // If one - means something is available.
            Unk2 = MemStream.ReadBit();
            Debug.Assert(Unk2 == 0, "We expect one here. This has extra data!");

            // Vector3? - FLOATS
            Unk3 = new int[3];
            for (int i = 0; i < Unk3.Length; i++)
            {
                Unk3[i] = MemStream.ReadInt32();
            }

            // Read Hashes. Fixed number of 2.
            bool bUnk4HashesAvailable = MemStream.ReadBit();
            if (bUnk4HashesAvailable)
            {
                Unk4 = new ulong[2];
                for (int i = 0; i < Unk4.Length; i++)
                {
                    Unk4[i] = MemStream.ReadUInt64();
                }
            }

            // Read unknown nested data
            Unk5 = MemStream.ReadBit();
            if(Unk5 == 1)
            {
                Unk6.Load(MemStream);
            }
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt32(Unk0);

            // Transform?
            Unk1.Save(MemStream);

            MemStream.WriteBit(Unk2);

            // Vector3?
            foreach (int Value in Unk3)
            {
                MemStream.WriteInt32(Value);
            }

            // Only saveable if we have some present
            bool bUnk4HashesAvailable = (Unk4 != null  && Unk4.Length > 0);
            MemStream.WriteBit(bUnk4HashesAvailable);

            if(bUnk4HashesAvailable)
            {
                // fixed number of 2
                foreach (ulong Value in Unk4)
                {
                    MemStream.WriteUInt64(Value);
                }
            }

            // Save unknown nested data
            MemStream.WriteBit(Unk5);
            if(Unk5 == 1)
            {
                Unk6.Save(MemStream);
            }
        }
    }
}
