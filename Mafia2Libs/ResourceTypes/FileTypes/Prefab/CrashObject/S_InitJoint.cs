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
        public C_Transform Unk4 { get; set; } // transform 0
        public C_Transform Unk5 { get; set; } // transform 1
        public byte Unk6 { get; set; }
        public int[] Unk7 { get; set; } // Vector3?
        public uint Unk8 { get; set; }

        public S_InitJoint()
        {
            JointSets = new S_InitJointSet[0];
            Unk4 = new C_Transform();
            Unk5 = new C_Transform();
            Unk7 = new int[0];
        }

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

            // Read Matrices
            Unk4.Load(MemStream);
            Unk5.Load(MemStream);

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

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt16(Unk0);
            MemStream.WriteUInt16(Unk1);
            MemStream.WriteUInt16(Unk2);
            MemStream.WriteUInt16(Unk3);

            // Write Joints
            MemStream.WriteUInt32((uint)JointSets.Length);
            foreach(S_InitJointSet JointSet in JointSets)
            {
                JointSet.Save(MemStream);
            }

            // Write Matrices
            Unk4.Save(MemStream);
            Unk5.Save(MemStream);

            MemStream.WriteBit(Unk6);

            // Vector3?
            foreach (int Value in Unk7)
            {
                MemStream.WriteInt32(Value);
            }

            MemStream.WriteUInt32(Unk8);
        }
    }
}
