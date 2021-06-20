using BitStreams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_InitWheel
    {
        public int[] Matrix { get; set; }

        public void Load(BitStream MemStream)
        {
            int Unk0 = MemStream.ReadInt32();
            int Unk1 = MemStream.ReadInt32();

            // TODO: Check this
            uint NumArms = MemStream.ReadUInt32(); // Could be count
            if(NumArms > 0)
            {
                int z = 0;
            }

            Matrix = new int[12];
            for(uint i = 0; i < Matrix.Length; i++)
            {
                Matrix[i] = MemStream.ReadInt32();
            }

            int Unk2 = MemStream.ReadInt32();
            int Unk3 = MemStream.ReadInt32(); // Vector3 X?
            int Unk4 = MemStream.ReadInt32(); // Vector3 Y?
            int Unk5 = MemStream.ReadInt32(); // Vector3 Z?

            int Unk6 = MemStream.ReadInt32();
            int Unk7 = MemStream.ReadInt32();
            int Unk8 = MemStream.ReadInt32();
            int Unk9 = MemStream.ReadInt32();
            int Unk10 = MemStream.ReadInt32();
        }
    }
}
