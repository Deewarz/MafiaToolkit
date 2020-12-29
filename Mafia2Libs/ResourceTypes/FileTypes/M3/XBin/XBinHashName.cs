﻿using Gibbed.Illusion.FileFormats.Hashing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using Utils.Extensions;
using Utils.Helpers;
using Utils.Helpers.Reflection;
using Utils.Language;

namespace ResourceTypes.M3.XBin
{
    public static class XBinHashStorage
    {
        static Dictionary<ulong, string> HashStorage;

        public static void LoadStorage()
        {
            HashStorage = new Dictionary<ulong, string>();
            HashStorage.Add(14695981039346656037, "");

            string[] LoadedLines = File.ReadAllLines("Resources//GameData//XBin_Hashes.txt");

            foreach(string Line in LoadedLines)
            {
                ulong FNVHash = FNV64.Hash(Line);
                HashStorage.TryAdd(FNVHash, Line);
            }
        }

        public static string GetNameFromHash(ulong Hash)
        {
            return HashStorage.TryGet(Hash);
        }
    }

    public class XBinHashNameConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(XBinHashName) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            object Result = null;
            string StringValue = value as string;

            string[] Splits = StringValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            ulong Hash = 0;
            bool bIsOnlyUlong = ulong.TryParse(Splits[0], out Hash);

            XBinHashName HashName = new XBinHashName();

            // If it is indeed only the hash, quickly return out
            if (bIsOnlyUlong)
            {
                HashName.Hash = Hash;
                Result = HashName;
                return Result ?? base.ConvertFrom(context, culture, value);
            }

            string RemovedBrackets = Splits[1].Replace("[", "");
            RemovedBrackets = Splits[1].Replace("]", "");

            HashName.Name = Splits[0];
            HashName.Hash = ulong.Parse(RemovedBrackets);

            return Result ?? base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            object result = null;
            XBinHashName HashName = (XBinHashName)value;

            if (destinationType == typeof(string))
            {
                result = HashName.ToString();
            }

            return result ?? base.ConvertTo(context, culture, value, destinationType);
        }
    }

    [TypeConverter(typeof(XBinHashNameConverter)), PropertyClassAllowReflection()]
    public class XBinHashName
    {
        private ulong _hash;

        [PropertyForceAsAttribute]
        public ulong Hash {
            get { return _hash; }
            set { SetHash(value); }
        }
        [LocalisedDescription("$XBIN_PROP_DESC_NAME_UNSTORED"), PropertyForceAsAttribute]
        public string Name { get; set; }

        public XBinHashName()
        {
            Hash = 0;
            Name = "";
        }

        public void ReadFromFile(BinaryReader reader)
        {
            Hash = reader.ReadUInt64();
            Name = XBinHashStorage.GetNameFromHash(Hash);
        }

        public void WriteToFile(BinaryWriter writer)
        {
            writer.Write(Hash);
        }

        public void SetHash(ulong Value)
        {
            _hash = Value;
            Name = XBinHashStorage.GetNameFromHash(Value);
        }

        public override string ToString()
        {
            if(!string.IsNullOrEmpty(Name))
            {
                return string.Format("{0} [{1}]", Name, Hash);
            }

            return Hash.ToString();
        }
    }
}
