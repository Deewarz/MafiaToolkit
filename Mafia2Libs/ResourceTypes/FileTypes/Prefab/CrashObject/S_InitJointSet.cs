using BitStreams;
using System;
using System.Diagnostics;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_InitJointSet
    {
        public class DataPacket
        {
            // bunch of unknowns (possible all floats?)
            public int[] Unk0 { get; set; }

            public DataPacket()
            {
                Unk0 = new int[9];
            }

            public void Load(BitStream MemStream)
            {
                // fixed size
                Unk0 = new int[9];
                for(int i = 0; i < Unk0.Length; i++)
                {
                    Unk0[i] = MemStream.ReadInt32();
                }
            }

            public void Save(BitStream MemStream)
            {
                foreach(int Value in Unk0)
                {
                    MemStream.WriteInt32(Value);
                }
            }
        }

        public uint Unk0 { get; set; } // fixed int
        public int Unk1 { get; set; } // float
        public DataPacket[] Unk2 { get; set; } // 6

        public S_InitJointSet()
        {
            Unk2 = new DataPacket[6];
            for (int i = 0; i < Unk2.Length; i++)
            {
                Unk2[i] = new DataPacket();
            }
        }

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt32();
            Unk1 = MemStream.ReadInt32();

            // fixed size
            Unk2 = new DataPacket[6];
            for(int i = 0;  i < Unk2.Length; i++)
            {
                DataPacket NewPacket = new DataPacket();
                NewPacket.Load(MemStream);
                Unk2[i] = NewPacket;
            }
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt32(Unk0);
            MemStream.WriteInt32(Unk1);

            foreach(DataPacket Value in Unk2)
            {
                Value.Save(MemStream);
            }
        }
    }
}
