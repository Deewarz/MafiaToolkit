﻿using BitStreams;
using Utils.Helpers.Reflection;

namespace ResourceTypes.Prefab.Vehicle
{
    public class S_InitSkinZoneGroup
    {
        [PropertyForceAsAttribute]
        public ushort MaterialGroup { get; set; }
        [PropertyForceAsAttribute]
        public ushort SkinZoneRange { get; set; }
        [PropertyForceAsAttribute]
        public ushort SkinZoneGroupIndex { get; set; }

        public void Load(BitStream MemStream)
        {
            MaterialGroup = MemStream.ReadUInt16();
            SkinZoneRange = MemStream.ReadUInt16();
            SkinZoneGroupIndex = MemStream.ReadUInt16();
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt16(MaterialGroup);
            MemStream.WriteUInt16(SkinZoneRange);
            MemStream.WriteUInt16(SkinZoneGroupIndex);
        }
    }
}
