using BitStreams;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_InitDeformPartEffect_Pack
    {
        public ushort Unk0 { get; set; } // usually 65535
        public ushort Unk1 { get; set; } // usually 65535
        public int Unk2 { get; set; } // float
        public int Unk3 { get; set; } // float
        public int Unk4 { get; set; } // float
        public int Unk5 { get; set; } // float
        public ushort Unk6 { get; set; } // usually 65535

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt16();
            Unk1 = MemStream.ReadUInt16();
            Unk2 = MemStream.ReadInt32();
            Unk3 = MemStream.ReadInt32();
            Unk4 = MemStream.ReadInt32();
            Unk5 = MemStream.ReadInt32();
            Unk6 = MemStream.ReadUInt16();
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt16(Unk0);
            MemStream.WriteUInt16(Unk1);
            MemStream.WriteInt32(Unk2);
            MemStream.WriteInt32(Unk3);
            MemStream.WriteInt32(Unk4);
            MemStream.WriteInt32(Unk5);
            MemStream.WriteUInt16(Unk6);
        }
    }

    public class S_InitDeformPartEffects
    {
        public int[] Unk0 { get; set; } // transform?
        public S_InitDeformPartEffect_Pack[] Unk1 { get; set; } // FIXED ARRAY TO 3
        public ushort Unk2 { get; set; }
        public ushort Unk3 { get; set; }
        public ushort Unk4 { get; set; }
        public int Unk5 { get; set; }
        public int Unk6 { get; set; }
        public int Unk7 { get; set; }
        public int Unk8 { get; set; }
        public ushort Unk9 { get; set; }
        public ushort Unk10 { get; set; }
        public ushort Unk11 { get; set; }
        public ushort Unk12 { get; set; }
        public ushort Unk13 { get; set; }
        public ushort Unk14 { get; set; }
        public int Unk15 { get; set; }
        public int Unk16 { get; set; }
        public int Unk17 { get; set; }
        public int Unk18 { get; set; }
        public ushort Unk19 { get; set; }
        public ushort Unk20 { get; set; }
        public ushort Unk21 { get; set; }
        public ushort Unk22 { get; set; }
        public ushort Unk23 { get; set; }
        public ushort Unk24 { get; set; }
        public ushort Unk25 { get; set; }
        public ushort Unk26 { get; set; }
        public ushort Unk27 { get; set; }
        public ushort Unk28 { get; set; }
        public ushort Unk29 { get; set; }
        public ushort Unk30 { get; set; }
        public ushort Unk31 { get; set; }
        public ushort Unk32 { get; set; }
        public ushort Unk33 { get; set; }
        public byte Unk34 { get; set; }
        public int Unk35 { get; set; }
        public int Unk36 { get; set; }
        public int Unk37 { get; set; }
        public int Unk38 { get; set; }
        public int Unk39 { get; set; }

        public void Load(BitStream MemStream)
        {
            // BitStream type of something
            // I think its transform (floats)
            Unk0 = new int[12];
            for (int i = 0; i < Unk0.Length; i++)
            {
                Unk0[i] = MemStream.ReadInt32();
            }

            // packet of data (what is it?)
            Unk1 = new S_InitDeformPartEffect_Pack[3];
            for (int i = 0; i < Unk1.Length; i++)
            {
                S_InitDeformPartEffect_Pack NewPack = new S_InitDeformPartEffect_Pack();
                NewPack.Load(MemStream);
                Unk1[i] = NewPack;
            }

            Unk2 = MemStream.ReadUInt16();
            Unk3 = MemStream.ReadUInt16();
            Unk4 = MemStream.ReadUInt16();
            Unk5 = MemStream.ReadInt32();
            Unk6 = MemStream.ReadInt32();
            Unk7 = MemStream.ReadInt32();
            Unk8 = MemStream.ReadInt32();
            Unk9 = MemStream.ReadUInt16();
            Unk10 = MemStream.ReadUInt16();
            Unk11 = MemStream.ReadUInt16();
            Unk12 = MemStream.ReadUInt16();
            Unk13 = MemStream.ReadUInt16();
            Unk14 = MemStream.ReadUInt16();
            Unk15 = MemStream.ReadInt32();
            Unk16 = MemStream.ReadInt32();
            Unk17 = MemStream.ReadInt32();
            Unk18 = MemStream.ReadInt32();
            Unk19 = MemStream.ReadUInt16();
            Unk20 = MemStream.ReadUInt16();
            Unk21 = MemStream.ReadUInt16();
            Unk22 = MemStream.ReadUInt16();
            Unk23 = MemStream.ReadUInt16();
            Unk24 = MemStream.ReadUInt16();
            Unk25 = MemStream.ReadUInt16();
            Unk26 = MemStream.ReadUInt16();
            Unk27 = MemStream.ReadUInt16();
            Unk28 = MemStream.ReadUInt16();
            Unk29 = MemStream.ReadUInt16();
            Unk30 = MemStream.ReadUInt16();
            Unk31 = MemStream.ReadUInt16();
            Unk32 = MemStream.ReadUInt16();
            Unk33 = MemStream.ReadUInt16();
            Unk34 = MemStream.ReadBit();
            Unk35 = MemStream.ReadInt32();
            Unk36 = MemStream.ReadInt32();
            Unk37 = MemStream.ReadInt32();
            Unk38 = MemStream.ReadInt32();
            Unk39 = MemStream.ReadInt32();
        }

        public void Save(BitStream MemStream)
        {
            // BitStream type of something
            // I think its transform (floats)
            foreach (int Value in Unk0)
            {
                MemStream.WriteInt32(Value);
            }

            // Write whatever this is
            foreach (S_InitDeformPartEffect_Pack Value in Unk1)
            {
                Value.Save(MemStream);
            }

            MemStream.WriteUInt16(Unk2);
            MemStream.WriteUInt16(Unk3);
            MemStream.WriteUInt16(Unk4);
            MemStream.WriteInt32(Unk5);
            MemStream.WriteInt32(Unk6);
            MemStream.WriteInt32(Unk7);
            MemStream.WriteInt32(Unk8);
            MemStream.WriteUInt16(Unk9);
            MemStream.WriteUInt16(Unk10);
            MemStream.WriteUInt16(Unk11);
            MemStream.WriteUInt16(Unk12);
            MemStream.WriteUInt16(Unk13);
            MemStream.WriteUInt16(Unk14);
            MemStream.WriteInt32(Unk15);
            MemStream.WriteInt32(Unk16);
            MemStream.WriteInt32(Unk17);
            MemStream.WriteInt32(Unk18);
            MemStream.WriteUInt16(Unk19);
            MemStream.WriteUInt16(Unk20);
            MemStream.WriteUInt16(Unk21);
            MemStream.WriteUInt16(Unk22);
            MemStream.WriteUInt16(Unk23);
            MemStream.WriteUInt16(Unk24);
            MemStream.WriteUInt16(Unk25);
            MemStream.WriteUInt16(Unk26);
            MemStream.WriteUInt16(Unk27);
            MemStream.WriteUInt16(Unk28);
            MemStream.WriteUInt16(Unk29);
            MemStream.WriteUInt16(Unk30);
            MemStream.WriteUInt16(Unk31);
            MemStream.WriteUInt16(Unk32);
            MemStream.WriteUInt16(Unk33);
            MemStream.WriteBit(Unk34);
            MemStream.WriteInt32(Unk35);
            MemStream.WriteInt32(Unk36);
            MemStream.WriteInt32(Unk37);
            MemStream.WriteInt32(Unk38);
            MemStream.WriteInt32(Unk39);
        }
    }
}
