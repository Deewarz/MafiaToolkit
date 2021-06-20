using BitStreams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceTypes.Prefab.CrashObject
{
    public class C_GUID
    {
        public uint Part0 { get; set; }
        public uint Part1 { get; set; }

        public void Load(BitStream MemStream)
        {
            Part0 = MemStream.ReadUInt32();
            Part1 = MemStream.ReadUInt32();
        }
    }

    public class S_InitDeformMaterial
    {
        public C_GUID Unk0 { get; set; } // m_Guid
        public uint Unk1 { get; set; } // m_Group

        public void Load(BitStream MemStream)
        {
            Unk0 = new C_GUID();
            Unk0.Load(MemStream);

            Unk1 = MemStream.ReadUInt32();
        }
    }

    public class S_InitColorAndDirty
    {
        public C_GUID Unk0 { get; set; } // m_Guid
        public ulong Unk1 { get; set; } // m_TextureName
        public uint Unk2 { get; set; } // m_Flags

        public void Load(BitStream MemStream)
        {
            Unk0 = new C_GUID();
            Unk0.Load(MemStream);

            Unk1 = MemStream.ReadUInt64();
            Unk2 = MemStream.ReadUInt32();
        }
    }

    public class S_InitUnknownType
    {
        public ulong Unk0 { get; set; }
        public int Unk1 { get; set; }
        public int Unk2 { get; set; }
        public uint Unk3 { get; set; }
        public uint Unk4 { get; set; }
        public int Unk5 { get; set; } // float
        public int Unk6 { get; set; } // float
        public int Unk7 { get; set; } // float
        public int Unk8 { get; set; } // float
        public ulong[] Unk9 { get; set; }
        public ulong Unk10 { get; set; }
        public uint Unk11 { get; set; }
        public uint Unk12 { get; set; }

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt64();
            Unk1 = MemStream.ReadInt32();
            Unk2 = MemStream.ReadInt32();
            Unk3 = MemStream.ReadUInt32();
            Unk4 = MemStream.ReadUInt32();
            Unk5 = MemStream.ReadInt32(); // float
            Unk6 = MemStream.ReadInt32(); // float
            Unk7 = MemStream.ReadInt32(); // float
            Unk8 = MemStream.ReadInt32(); // float

            // Read array
            uint NumHashes = MemStream.ReadUInt32();
            Unk9 = new ulong[NumHashes];
            for (uint i = 0; i < Unk9.Length; i++)
            {
                Unk9[i] = MemStream.ReadUInt64();
            }

            Unk10 = MemStream.ReadUInt64();
            Unk11 = MemStream.ReadUInt32();
            Unk12 = MemStream.ReadUInt32();
        }
    }

    public class S_ShaderEffectInit
    {
        public ulong[] Unk0 { get; set; }// m_FGSCloneVisuals
        public S_InitDeformMaterial[] Unk1 { get; set; } // m_DeformMaterial
        public S_InitColorAndDirty[] Unk2 { get; set; }
        public S_InitUnknownType[] Unk3 { get; set; }

        public void Load(BitStream MemStream)
        {
            uint NumHashes = MemStream.ReadUInt32();
            Unk0 = new ulong[NumHashes];
            for (uint i = 0; i < Unk0.Length; i++)
            {
                Unk0[i] = MemStream.ReadUInt64();
            }

            // Read Deforms
            uint NumDeforms = MemStream.ReadUInt32();
            Unk1 = new S_InitDeformMaterial[NumDeforms];
            for (uint i = 0; i < Unk1.Length; i++)
            {
                S_InitDeformMaterial NewDeform = new S_InitDeformMaterial();
                NewDeform.Load(MemStream);

                Unk1[i] = NewDeform;
            }

            // Read Colour & Dirty
            uint NumColourAndDirty = MemStream.ReadUInt32();
            Unk2 = new S_InitColorAndDirty[NumColourAndDirty];
            for (uint i = 0; i < Unk2.Length; i++)
            {
                S_InitColorAndDirty NewColourAndDirty = new S_InitColorAndDirty();
                NewColourAndDirty.Load(MemStream);

                Unk2[i] = NewColourAndDirty;
            }

            // Read Unknown
            uint NumUnknown = MemStream.ReadUInt32();
            Unk3 = new S_InitUnknownType[NumUnknown];
            for (uint i = 0; i < NumUnknown; i++)
            {
                S_InitUnknownType NewUnknown = new S_InitUnknownType();
                NewUnknown.Load(MemStream);

                Unk3[i] = NewUnknown;
            }

            uint Unk4 = MemStream.ReadUInt32();
            uint Unk5 = MemStream.ReadUInt32();
            int Unk6 = MemStream.ReadInt32(); // float
            uint Unk7 = MemStream.ReadUInt32();
            uint Unk8 = MemStream.ReadUInt32();

            // Read SkinZonePartData
            uint NumSkinZonePartData = MemStream.ReadUInt32();
            S_InitSkinZonePartData[] Unk9 = new S_InitSkinZonePartData[NumSkinZonePartData];
            for (uint i = 0; i < Unk9.Length; i++)
            {
                S_InitSkinZonePartData SZPartData = new S_InitSkinZonePartData();
                SZPartData.Load(MemStream);
                Unk9[i] = SZPartData;
            }

            // Read S_InitSkinZoneGroup
            uint NumSkinZoneGroups = MemStream.ReadUInt32();
            S_InitSkinZoneGroup[] Unk10 = new S_InitSkinZoneGroup[NumSkinZoneGroups];
            for (uint i = 0; i < Unk10.Length; i++)
            {
                S_InitSkinZoneGroup SZGroup = new S_InitSkinZoneGroup();
                SZGroup.Load(MemStream);
                Unk10[i] = SZGroup;
            }

            C_GUID Guid = new C_GUID(); // m_SPZAndLightGUID
            Guid.Load(MemStream);

            int Unk11 = MemStream.ReadInt32(); // m_BoneStiffness (float)
        }
    }
}
