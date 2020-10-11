﻿using ResourceTypes.M3.XBin;
using System;
using System.IO;

namespace FileTypes.XBin.StreamMap.Commands
{
    public class Command_UnlockSDS : ICommand
    {
        private readonly uint Magic = 0x687DAD8B;

        public string SDSName { get; set; }

        public void ReadFromFile(BinaryReader reader)
        {
            SDSName = XBinCoreUtils.ReadStringPtrWithOffset(reader);
        }

        public void WriteToFile(BinaryWriter writer)
        {
            writer.Write(-1); // SDSName
        }
        public int GetSize()
        {
            return 4;
        }
        public uint GetMagic()
        {
            return Magic;
        }
    }
}
