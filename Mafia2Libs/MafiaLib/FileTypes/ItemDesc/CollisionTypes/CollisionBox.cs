﻿using System.IO;
using SharpDX;

namespace Mafia2
{
    public class CollisionBox
    {
        Vector3 vector;

        public CollisionBox(BinaryReader reader)
        {
            ReadFromFile(reader);
        }

        public void ReadFromFile(BinaryReader reader)
        {
            vector = Vector3Extenders.ReadFromFile(reader);
        }

        public void WriteToFile(BinaryWriter writer)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
        }
    }
}
