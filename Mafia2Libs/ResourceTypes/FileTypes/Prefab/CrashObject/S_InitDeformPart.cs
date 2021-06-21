using BitStreams;
using System;
using System.Diagnostics;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_InitDeformPart_Unk12Pack
    {
        public uint Unk01 { get; set; } // numeric
        public int Unk02 { get; set; } // float

        public void Load(BitStream MemStream)
        {
            Unk01 = MemStream.ReadUInt32();
            Unk02 = MemStream.ReadInt32();
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt32(Unk01);
            MemStream.WriteInt32(Unk02);
        }
    }

    public class S_InitDeformPart
    {
        public uint Unk0 { get; set; }
        public uint Unk1 { get; set; }
        public byte Unk2 { get; set; }
        public ulong[] Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int Unk5 { get; set; }
        public int Unk6 { get; set; }
        public int Unk7 { get; set; }
        public int Unk8 { get; set; }
        public int Unk9 { get; set; }
        public uint Unk10 { get; set; }
        public uint Unk11 { get; set; }
        public S_InitDeformPart_Unk12Pack[] Unk12 { get; set; }
        public uint Unk13 { get; set; }
        public S_InitCollVolume[] CollisionVolumes { get; set; }
        public S_InitSMDeformBone[] SMDeformBones { get; set; }
        public ushort[] Unk14 { get; set; } // indexes?
        public int[] Unk15 { get; set; } // transform?
        public ulong Unk16 { get; set; }
        public ushort Unk17 { get; set; }
        public byte Unk18 { get; set; }
        public byte Unk19 { get; set; }
        public ushort[] Unk20 { get; set; } // indexes?
        public byte Unk21 { get; set; } // flag to check whether some data is available
        public byte Unk22 { get; set; } // flag to check whether some data is available
        public uint Unk23 { get; set; }
        public uint Unk24 { get; set; }
        public S_InitDeformPartCommon CommonData { get; set; }

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt32();
            Unk1 = MemStream.ReadUInt32();
            Unk2 = MemStream.ReadBit();

            // collect hashes
            Unk3 = PrefabUtils.ReadHashArray(MemStream);

            Unk4 = MemStream.ReadInt32(); // float
            Unk5 = MemStream.ReadInt32(); // float
            Unk6 = MemStream.ReadInt32(); // float
            Unk7 = MemStream.ReadInt32(); // float
            Unk8 = MemStream.ReadInt32(); // float
            Unk9 = MemStream.ReadInt32(); // float

            Unk10 = MemStream.ReadUInt32(); // Count
            Unk11 = MemStream.ReadUInt32(); // Count

            // Read unknown data
            uint Unk12Count = MemStream.ReadUInt32(); // Count
            Unk12 = new S_InitDeformPart_Unk12Pack[Unk12Count];
            for (int i = 0; i < Unk12Count; i++)
            {
                S_InitDeformPart_Unk12Pack NewPack = new S_InitDeformPart_Unk12Pack();
                NewPack.Load(MemStream);
                Unk12[i] = NewPack;
            }
            
            // NB: Could be related to collision volumes
            Unk13 = MemStream.ReadUInt32(); // Count

            // Read collision volumes
            uint NumCollisionVolumes = MemStream.ReadUInt32();
            CollisionVolumes = new S_InitCollVolume[NumCollisionVolumes];
            for (int i = 0; i < NumCollisionVolumes; i++)
            {
                S_InitCollVolume CollisionVolume = new S_InitCollVolume();
                CollisionVolume.Load(MemStream);
                CollisionVolumes[i] = CollisionVolume;
            }

            // Read SM Deform bones
            uint NumSMDeformBones = MemStream.ReadUInt32();
            SMDeformBones = new S_InitSMDeformBone[NumSMDeformBones];
            for (int i = 0; i < NumSMDeformBones; i++)
            {
                S_InitSMDeformBone DeformBone = new S_InitSMDeformBone();
                DeformBone.Load(MemStream);
                SMDeformBones[i] = DeformBone;
            }

            // UInt16s - indexes?
            uint NumIndexes = MemStream.ReadUInt32();
            Unk14 = new ushort[NumIndexes];
            for (int i = 0; i < NumIndexes; i++)
            {
                Unk14[i] = MemStream.ReadUInt16();
            }

            // BitStream type of something
            // I think its transform (floats)
            Unk15 = new int[12];
            for (int i = 0; i < Unk15.Length; i++)
            {
                Unk15[i] = MemStream.ReadInt32();
            }

            Unk16 = MemStream.ReadUInt64();
            Unk17 = MemStream.ReadUInt16();
            Unk18 = MemStream.ReadByte();
            Unk19 = MemStream.ReadByte();

            // UInt16s - indexes?
            uint NumIndexes2 = MemStream.ReadUInt32();
            Unk20 = new ushort[NumIndexes2];
            for (int i = 0; i < NumIndexes2; i++)
            {
                Unk20[i] = MemStream.ReadUInt16();
            }

            // If one - means something is available.
            Unk21 = MemStream.ReadBit();
            Debug.Assert(Unk21 == 0, "We expect one here. This has extra data!");

            // If one - means something is available.
            Unk22 = MemStream.ReadBit();
            Debug.Assert(Unk22 == 0, "We expect one here. This has extra data!");

            Unk23 = MemStream.ReadUInt32(); // 0?
            Unk24 = MemStream.ReadUInt32(); // 0?

            CommonData = new S_InitDeformPartCommon();
            CommonData.Load(MemStream);
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt32(Unk0);
            MemStream.WriteUInt32(Unk1);
            MemStream.WriteBit(Unk2);

            PrefabUtils.WriteHashArray(MemStream, Unk3);

            MemStream.WriteInt32(Unk4);
            MemStream.WriteInt32(Unk5);
            MemStream.WriteInt32(Unk6);
            MemStream.WriteInt32(Unk7);
            MemStream.WriteInt32(Unk8);
            MemStream.WriteInt32(Unk9);

            MemStream.WriteUInt32(Unk10);
            MemStream.WriteUInt32(Unk11);

            // Write Unknown data
            MemStream.WriteUInt32((uint)Unk12.Length);
            foreach(S_InitDeformPart_Unk12Pack Value in Unk12)
            {
                Value.Save(MemStream);
            }

            MemStream.WriteUInt32(Unk13);
            
            // Write Collision Volumes
            MemStream.WriteUInt32((uint)CollisionVolumes.Length);
            foreach (S_InitCollVolume Value in CollisionVolumes)
            {
                Value.Save(MemStream);
            }

            // Write SM Deform bones
            MemStream.WriteUInt32((uint)SMDeformBones.Length);
            foreach (S_InitSMDeformBone Value in SMDeformBones)
            {
                Value.Save(MemStream);
            }

            // UInt16s - indexes?
            MemStream.WriteUInt32((uint)Unk14.Length);
            foreach (ushort Value in Unk14)
            {
                MemStream.WriteUInt16(Value);
            }

            // BitStream type of something
            // I think its transform (floats)
            foreach (int Value in Unk15)
            {
                MemStream.WriteInt32(Value);
            }

            MemStream.WriteUInt64(Unk16);
            MemStream.WriteUInt16(Unk17);
            MemStream.WriteByte(Unk18);
            MemStream.WriteByte(Unk19);

            // UInt20s - indexes?
            MemStream.WriteUInt32((uint)Unk20.Length);
            foreach (ushort Value in Unk20)
            {
                MemStream.WriteUInt16(Value);
            }

            MemStream.WriteBit(Unk21);
            MemStream.WriteBit(Unk22);
            MemStream.WriteUInt32(Unk23);
            MemStream.WriteUInt32(Unk24);

            CommonData.Save(MemStream);

            return;
        }
    }
}
