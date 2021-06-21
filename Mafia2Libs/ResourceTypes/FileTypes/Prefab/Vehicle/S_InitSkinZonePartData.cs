using BitStreams;

namespace ResourceTypes.Prefab.Vehicle
{
    public class S_InitSkinZoneSettings
    {
        public ushort SkinZoneIndex { get; set; }
        public ushort MaterialGroupIndex { get; set; }
        public float Intensity { get; set; }

        public void Load(BitStream MemStream)
        {
            SkinZoneIndex = MemStream.ReadUInt16();
            MaterialGroupIndex = MemStream.ReadUInt16();
            Intensity = MemStream.ReadSingle();
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt16(SkinZoneIndex);
            MemStream.WriteUInt16(MaterialGroupIndex);
            MemStream.WriteSingle(Intensity);
        }
    }

    public class S_InitSkinZoneFrameData
    {
        public ulong FrameName { get; set; }
        public S_InitSkinZoneSettings[] SZSettings { get; set; }

        public void Load(BitStream MemStream)
        {
            FrameName = MemStream.ReadUInt64();

            uint NumSkinZoneSettings = MemStream.ReadUInt32();
            SZSettings = new S_InitSkinZoneSettings[NumSkinZoneSettings];
            for (uint i = 0; i < SZSettings.Length; i++)
            {
                S_InitSkinZoneSettings SZSetting = new S_InitSkinZoneSettings();
                SZSetting.Load(MemStream);
                SZSettings[i] = SZSetting;
            }
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt64(FrameName);

            MemStream.WriteUInt32((uint)SZSettings.Length);
            foreach(S_InitSkinZoneSettings Setting in SZSettings)
            {
                Setting.Save(MemStream);
            }
        }

    }

    public class S_InitSkinZonePartData
    {
        public ulong MainPartFrameName { get; set; }
        public S_InitSkinZoneFrameData[] SkinZoneFrameData { get; set; }

        public void Load(BitStream MemStream)
        {
            MainPartFrameName = MemStream.ReadUInt64();

            uint NumInitSkinZoneFrameData = MemStream.ReadUInt32();
            SkinZoneFrameData = new S_InitSkinZoneFrameData[NumInitSkinZoneFrameData];         
            for(uint i = 0; i < SkinZoneFrameData.Length; i++)
            {
                S_InitSkinZoneFrameData NewSZFrameData = new S_InitSkinZoneFrameData();
                NewSZFrameData.Load(MemStream);
                SkinZoneFrameData[i] = NewSZFrameData;
            }
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt64(MainPartFrameName);

            MemStream.WriteUInt32((uint)SkinZoneFrameData.Length);
            foreach (S_InitSkinZoneFrameData SZFrameData in SkinZoneFrameData)
            {
                SZFrameData.Save(MemStream);
            }
        }
    }
}
