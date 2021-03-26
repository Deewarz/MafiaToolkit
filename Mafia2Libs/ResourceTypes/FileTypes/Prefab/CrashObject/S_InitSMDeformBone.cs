using BitStreams;
using System;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_InitSMDeformBone
    {
        public ulong Unk0 { get; set; } // m_SMJointName
        public int[] Unk1 { get; set; } // m_OrigPos[0-2], m_Range[3-5], m_MoveAccumulator[6-8]
        public int Unk2 { get; set; } // m_Intensity
        public int Unk3 { get; set; } // m_CRadius

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt64();

            // ALL FLOATS
            Unk1 = new int[9];
            for (int i = 0; i < 9; i++)
            {
                Unk1[i] = MemStream.ReadInt32();
            }

            // BOTH FLOATS
            Unk2 = MemStream.ReadInt32();
            Unk3 = MemStream.ReadInt32();
        }
    }
}
