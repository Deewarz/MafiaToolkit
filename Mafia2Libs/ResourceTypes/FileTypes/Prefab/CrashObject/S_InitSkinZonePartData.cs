using BitStreams;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_InitSkinZoneSettings
    {
        public ushort Unk1 { get; set; } // m_SkinZoneIndex
        public ushort Unk2 { get; set; } // m_MaterialGroupIndex
        public int Unk3 { get; set; } // m_Intensity (float)

        public void Load(BitStream MemStream)
        {
            Unk1 = MemStream.ReadUInt16();
            Unk2 = MemStream.ReadUInt16();
            Unk3 = MemStream.ReadInt32();
        }
    }

    public class S_InitSkinZoneFrameData
    {
        public ulong Unk0 { get; set; } // m_FrameName
        public S_InitSkinZoneSettings[] Unk1 { get; set; } // m_SZSettings

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt64();

            uint NumSkinZoneSettings = MemStream.ReadUInt32();
            Unk1 = new S_InitSkinZoneSettings[NumSkinZoneSettings];
            for (uint i = 0; i < Unk1.Length; i++)
            {
                S_InitSkinZoneSettings SZSettings = new S_InitSkinZoneSettings();
                SZSettings.Load(MemStream);
                Unk1[i] = SZSettings;
            }
        }
    }

    public class S_InitSkinZonePartData
    {
        public ulong Unk0 { get; set; } // m_MainPartFrameName
        public S_InitSkinZoneFrameData[] Unk1 { get; set; }

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt64();

            uint NumInitSkinZoneFrameData = MemStream.ReadUInt32();
            Unk1 = new S_InitSkinZoneFrameData[NumInitSkinZoneFrameData];         
            for(uint i = 0; i < Unk1.Length; i++)
            {
                S_InitSkinZoneFrameData NewSZFrameData = new S_InitSkinZoneFrameData();
                NewSZFrameData.Load(MemStream);
                Unk1[i] = NewSZFrameData;
            }
        }
    }
}
