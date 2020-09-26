﻿using System.IO;
using Gibbed.IO;
using Gibbed.Illusion.FileFormats;
using System.Collections.Generic;
using System.Windows;
using System;

namespace Gibbed.Mafia2.ResourceFormats
{
    public class GenericResource : IResourceType
    {
        readonly Dictionary<ulong, string> TypeExtensionMagic = new Dictionary<ulong, string>()
        {
            { 0x15B770C22,  ".vi.compiled" },
            { 0xA53038C9,  ".flownode" },
            { 0xC3A9C338,  ".dlgsel" },
            { 0x164D0E75C,  ".ires.compiled" },
            { 0xA757FB5364D0E75C,  ".ires.[nomesh].compiled" },
            { 0x222FDF7264D0E75C,  ".ires.[lod0].compiled" },
            { 0x222FDF7364D0E75C,  ".ires.[lod1].compiled" },
            { 0x222FDF7064D0E75C,  ".ires.[lod2].compiled" },
            { 0x1B4347D18,   ".entity.compiled" },
            { 0x4DE17E9B,  ".gbin" },
            { 0x45F07C8B,  ".gxml" },
            { 0x172D9EA8F, ".scene.compiled" },
            { 0x16AD0740B, ".collision.compiled" },
            { 0xA757FB5372D9EA8F,  ".scene.[nomesh].compiled" },
            { 0x222FDF7272D9EA8F,  ".scene.[lod0].compiled" },
            { 0x222FDF7372D9EA8F,  ".scene.[lod1].compiled" },
            { 0x222FDF7072D9EA8F,  ".scene.[lod2].compiled" },
            { 0x1E5CA8123,  ".streaming.compiled" },
            { 0x16024B481,  ".bpdb.compiled" },
            { 0x57572DAC256CA1DB,  ".trb.[global].compiled" },
            { 0x1256CA1DB, ".trb.compiled" },
            { 0xA27F694D, ".iprofai" },
            { 0x4A336D64, ".iproftime" },
            { 0x73CB32C9, ".fmv.compiled.effects" },
        };

        readonly Dictionary<string, ulong> TypeExtensionString = new Dictionary<string, ulong>()
        {
            { ".vi.compiled", 0x15B770C22 },
            { ".flownode", 0xA53038C9 },
            { ".dlgsel", 0xC3A9C338 },
            { ".ires.compiled", 0x164D0E75C },
            { ".ires.[nomesh].compiled", 0xA757FB5364D0E75C },
            { ".ires.[lod0].compiled", 0x222FDF7264D0E75C },
            { ".ires.[lod1].compiled", 0x222FDF7364D0E75C },
            { ".ires.[lod2].compiled", 0x222FDF7064D0E75C },
            { ".entity.compiled", 0x1B4347D18 },
            { ".gbin", 0x4DE17E9B },
            { ".gxml", 0x45F07C8B },
            { ".scene.compiled", 0x172D9EA8F },
            { ".collision.compiled", 0x16AD0740B },
            { ".scene.[nomesh].compiled", 0xA757FB5372D9EA8F },
            { ".scene.[lod0].compiled", 0x222FDF7272D9EA8F },
            { ".scene.[lod1].compiled", 0x222FDF7372D9EA8F },
            { ".scene.[lod2].compiled", 0x222FDF7072D9EA8F },
            { ".streaming.compiled", 0x1E5CA8123 },
            { ".bpdb.compiled", 0x16024B481 },
            { ".trb.[global].compiled", 0x57572DAC256CA1DB },
            { ".trb.compiled", 0x1256CA1DB },
            { ".iprofai", 0xA27F694D },
            { ".iproftime", 0x4A336D64 },
            { ".fmv", 0x428F61D4 },
        };


        public ulong GenericType;
        public ushort Unk0;
        public string DebugName;
        public byte[] Data;

        public void Serialize(ushort version, Stream output, Endian endian)
        {
            GenericType = DetermineMagic(DebugName);

            output.WriteValueU64(GenericType);
            output.WriteValueU16(Unk0);
            output.WriteValueU16(0);
            output.WriteBytes(Data);
        }

        public void Deserialize(ushort version, Stream input, Endian endian)
        {
            GenericType = input.ReadValueU64();
            Unk0 = input.ReadValueU16();
            DebugName = input.ReadStringU16(endian);

            // We do not have any size so we do (FILE_LENGTH - CURRENT_POS);
            Data = input.ReadBytes((int)(input.Length - input.Position));
        }

        public ulong DetermineMagic(string name)
        {
            string extension = GetFullExtensionUtil(name);
            ulong magic = 0;

            if(TypeExtensionString.ContainsKey(extension))
            {
                magic = TypeExtensionString[extension];
            }
            else
            {
                MessageBox.Show("Detected an unknown extension!!! SDS will NOT work!", "Toolkit");
            }

            return magic;
        }

        public string DetermineName(string name)
        {
            bool bGotDebugName = false;

            // Our database tool has figured out this file name.
            // Return.
            // TODO: Consider an easier approach for this, maybe have a flag?
            if (!name.Contains("File_"))
            {
                string extension = GetFullExtensionUtil(name);
                if(!TypeExtensionString.ContainsKey(extension))
                {
                    Console.WriteLine("Detected missing extension from DB.");
                }
                return name;
            }

            // Make sure we use the debug name.
            if (!string.IsNullOrEmpty(DebugName))
            {
                name = DebugName;
                bGotDebugName = true;
            }

            if (!bGotDebugName)
            {
                string withoutExtension = Path.GetFileNameWithoutExtension(name);

                if(TypeExtensionMagic.ContainsKey(GenericType))
                {
                    string extension = TypeExtensionMagic[GenericType];
                    withoutExtension += extension;
                }
                else 
                {
                    withoutExtension += ".genr";
                    MessageBox.Show("Detected an unknown GENR type. Please contract Greavesy with SDS name.", "Toolkit");
                }

                name = withoutExtension;
            }

            return name;
        }

        private string GetFullExtensionUtil(string FileName)
        {
            int extensionStart = FileName.IndexOf(".");
            return FileName.Substring(extensionStart);
        }
    }
}
