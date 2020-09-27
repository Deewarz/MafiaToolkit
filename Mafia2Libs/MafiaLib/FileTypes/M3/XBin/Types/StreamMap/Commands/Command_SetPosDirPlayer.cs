﻿using SharpDX;
using System.IO;
using Utils.SharpDXExtensions;

namespace FileTypes.XBin.StreamMap.Commands
{
    public class Command_SetPosDirPlayer : ICommand
    {
        private readonly uint Magic = 0x72386E2B;

        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }

        public void ReadFromFile(BinaryReader reader)
        {
            Position = Vector3Extenders.ReadFromFile(reader);
            Direction = Vector3Extenders.ReadFromFile(reader);
        }

        public void WriteToFile(BinaryWriter writer)
        {
            Position.WriteToFile(writer);
            Direction.WriteToFile(writer);
        }
    }
}
