﻿using System;
using System.IO;
using Utils.Extensions;

namespace ResourceTypes.Cutscene.AnimEntities
{
    public class AeUnk13 : AeBase
    {
        public override void ReadFromFile(MemoryStream stream, bool isBigEndian)
        {
            base.ReadFromFile(stream, isBigEndian);
        }

        public override void WriteToFile(MemoryStream stream, bool isBigEndian)
        {
            base.WriteToFile(stream, isBigEndian);
        }
    }

    public class AeUnk13Data : AeBaseData
    {
        public int Unk02 { get; set; }
        public override void ReadFromFile(MemoryStream stream, bool isBigEndian)
        {
            base.ReadFromFile(stream, isBigEndian);
            Unk02 = stream.ReadInt32(isBigEndian);
        }

        public override void WriteToFile(MemoryStream stream, bool isBigEndian)
        {
            base.WriteToFile(stream, isBigEndian);
            stream.Write(Unk02, isBigEndian);
        }
    }
}
