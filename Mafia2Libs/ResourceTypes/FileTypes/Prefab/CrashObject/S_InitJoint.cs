using BitStreams;
using System;
using System.Diagnostics;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_InitJoint
    {
        public ushort Unk0 { get; set; }
        public ushort Unk1 { get; set; }
        public ushort Unk2 { get; set; }
        public ushort Unk3 { get; set; }
        public S_InitJointSet[] JointSets { get; set; }
        public int[] Unk4 { get; set; } // transform 0
        public int[] Unk5 { get; set; } // transform 1
        public byte Unk6 { get; set; }
        public int[] Unk7 { get; set; } // Vector3?
        public uint Unk8 { get; set; }
        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt16();
            Unk1 = MemStream.ReadUInt16();
            Unk2 = MemStream.ReadUInt16();
            Unk3 = MemStream.ReadUInt16();

            uint NumJointSets = MemStream.ReadUInt32();
            JointSets = new S_InitJointSet[NumJointSets];
            for (int i = 0; i < NumJointSets; i++)
            {
                S_InitJointSet NewSet = new S_InitJointSet();
                NewSet.Load(MemStream);
                JointSets[i] = NewSet;
            }

            // BitStream type of something
            // I think its transform (floats)
            Unk4 = new int[12];
            for (int i = 0; i < Unk4.Length; i++)
            {
                Unk4[i] = MemStream.ReadInt32();
            }

            // BitStream type of something
            // I think its transform (floats)
            Unk5 = new int[12];
            for (int i = 0; i < Unk5.Length; i++)
            {
                Unk5[i] = MemStream.ReadInt32();
            }

            // If one - means something is available.
            Unk6 = MemStream.ReadBit();
            Debug.Assert(Unk6 == 0, "We expect one here. This has extra data!");

            // Vector3? - FLOATS
            Unk7 = new int[3];
            for (int i = 0; i < Unk7.Length; i++)
            {
                Unk7[i] = MemStream.ReadInt32();
            }

            // If one - means something is available.
            Unk8 = MemStream.ReadUInt32();
            Debug.Assert(Unk8 == 0, "We expect one here. This has extra data!");
        }
    }
}
