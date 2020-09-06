﻿using System.Collections.Generic;
using System.IO;
using Utils.StringHelpers;
using Utils.Types;

namespace ResourceTypes.Materials
{
    public class Material_v57 : IMaterial
    {
        public byte Unk0 { get; set; }
        public byte Unk1 { get; set; }
        public byte Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int Unk5 { get; set; }
        public List<MaterialSampler_v57> Samplers { get; set; }

        public Material_v57()
        {
            Samplers = new List<MaterialSampler_v57>();
        }

        public override void ReadFromFile(BinaryReader reader, VersionsEnumerator version)
        {
            ulong materialHash = reader.ReadUInt64();
            string materialName = StringHelpers.ReadString32(reader);
            MaterialName.String = materialName;
            MaterialName.uHash = materialHash;

            Unk0 = reader.ReadByte();
            Unk1 = reader.ReadByte();

            Flags = (MaterialFlags)reader.ReadInt32();
            Unk3 = reader.ReadByte();
            Unk4 = reader.ReadInt32();
            Unk5 = reader.ReadInt32();

            ShaderID = reader.ReadUInt64();
            ShaderHash = reader.ReadUInt32();

            int parameterCount = reader.ReadInt32();
            Parameters = new List<MaterialParameter>();
            for (int i = 0; i != parameterCount; i++)
            {
                var param = new MaterialParameter(reader);
                Parameters.Add(param);
            }

            int samplerCount = reader.ReadInt32();
            Samplers = new List<MaterialSampler_v57>();
            for (int i = 0; i != samplerCount; i++)
            {
                var shader = new MaterialSampler_v57();
                shader.ReadFromFile(reader, version);
                Samplers.Add(shader);
            }
        }

        public override void WriteToFile(BinaryWriter writer, VersionsEnumerator version)
        {
            // Material Name doesn't use standard hex serialization.
            writer.Write(MaterialName.uHash);
            writer.WriteString32(MaterialName.String);

            // Unknown Values.
            writer.Write(Unk0);
            writer.Write(Unk1);
            writer.Write((int)Flags);
            writer.Write(Unk3);
            writer.Write(Unk4);
            writer.Write(Unk5);

            // Shader ID and Hash
            writer.Write(ShaderID);
            writer.Write(ShaderHash);

            // Shader Parameters
            writer.Write(Parameters.Count);
            foreach (var param in Parameters)
            {
                param.WriteToFile(writer);
            }

            // Shader Samplers
            writer.Write(Samplers.Count);
            foreach (var shader in Samplers)
            {
                shader.WriteToFile(writer, version);
            }
        }

        public override Hash GetTextureByID(string SamplerName)
        {
            foreach (var sampler in Samplers)
            {
                if (sampler.ID == SamplerName)
                {
                    Hash TextureFile = new Hash();
                    TextureFile.String = sampler.GetFileName();
                    TextureFile.uHash = sampler.GetFileHash();
                    return TextureFile;
                }
            }

            return null;
        }

        public override bool HasTexture(string Name)
        {
            foreach (var sampler in Samplers)
            {
                string FileNameLowerCase = sampler.GetFileName().ToLower();
                return FileNameLowerCase.Contains(Name);
            }

            return false;
        }
    }

    public class MaterialSampler_v57 : IMaterialSampler
    {
        public int[] UnkSet0 { get; set; }
        public Hash TextureName { get; set; }
        public byte TexType { get; set; }
        public byte UnkZero { get; set; }
        public int[] UnkSet1 { get; set; }

        public MaterialSampler_v57() : base()
        {
            UnkSet0 = new int[2];
            UnkSet1 = new int[2];
            TextureName = new Hash();
        }

        public override void ReadFromFile(BinaryReader reader, VersionsEnumerator version)
        {
            ID = new string(reader.ReadChars(4));
            UnkSet0 = new int[2];
            for (int i = 0; i < UnkSet0.Length; i++)
            {
                UnkSet0[i] = reader.ReadInt32();
            }
            ulong TextureHash = reader.ReadUInt64();
            TexType = reader.ReadByte();
            UnkZero = reader.ReadByte();
            SamplerStates = reader.ReadBytes(6);
            UnkSet1 = new int[2]; // these can have erratic values
            for (int i = 0; i < UnkSet1.Length; i++)
            {
                UnkSet1[i] = reader.ReadInt32();
            }
            TextureName.String = StringHelpers.ReadString32(reader);
            TextureName.uHash = TextureHash;
        }

        public override void WriteToFile(BinaryWriter writer, VersionsEnumerator version)
        {
            writer.Write(ID.ToCharArray());
            for (int i = 0; i < UnkSet0.Length; i++)
            {
                writer.Write(UnkSet0[i]);
            }
            writer.Write(TextureName.uHash);
            writer.Write(TexType);
            writer.Write(UnkZero);
            writer.Write(SamplerStates);

            for (int i = 0; i < UnkSet1.Length; i++)
            {
                writer.Write(UnkSet1[i]);
            }
            writer.WriteString32(TextureName.String);
        }

        public override string GetFileName()
        {
            return TextureName.String;
        }

        public override ulong GetFileHash()
        {
            return TextureName.uHash;
        }

        public override string ToString()
        {
            return string.Format("ID: {0} Name: {1}", ID, GetFileName());
        }
    }
}
