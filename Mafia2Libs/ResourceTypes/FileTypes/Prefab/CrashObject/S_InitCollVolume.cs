using BitStreams;
using System;
using System.Diagnostics;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_InitCollVolume
    {
        public uint Unk0 { get; set; }
        public int[] Unk1 { get; set; } // transform?
        public byte Unk2 { get; set; } // if 1 - means something is available
        public int[] Unk3 { get; set; } // Vector3?
        public ulong[] Unk4 { get; set; } // hashes?
        public byte Unk5 { get; set; } // if 1 - means something is available

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt32();

            // BitStream type of something
            // I think its transform (floats)
            Unk1 = new int[12];
            for(int i = 0; i < Unk1.Length; i++)
            {
                Unk1[i] = MemStream.ReadInt32();
            }

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

            // If one - means something is available.
            Unk5 = MemStream.ReadBit();
            Debug.Assert(Unk5 == 0, "We expect one here. This has extra data!");
        }
    }
}
