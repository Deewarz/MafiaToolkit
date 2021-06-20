using BitStreams;
using System;
using System.Diagnostics;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_DeformationInitData
    {
        public S_InitDeformPart[] DeformParts { get; set; }
        public S_InitJoint[] InitJoints { get; set; }
        public S_InitOwnerDeform[] OwnerDeforms { get; set; }

        public virtual void Load(BitStream MemStream)
        {
            int GlobalPrefabVersion = MemStream.ReadInt32();

            ulong Hash0 = MemStream.ReadUInt64();
            ulong Hash1 = MemStream.ReadUInt64();
            ulong Hash2 = MemStream.ReadUInt64();

            uint NumDeformParts = MemStream.ReadUInt32();
            DeformParts = new S_InitDeformPart[NumDeformParts];
            for (int i = 0; i < NumDeformParts; i++)
            {
                S_InitDeformPart DeformPart = new S_InitDeformPart();
                DeformPart.Load(MemStream);
                DeformParts[i] = DeformPart;
            }

            uint NumJoints = MemStream.ReadUInt32();
            InitJoints = new S_InitJoint[NumJoints];
            for (int i = 0; i < InitJoints.Length; i++)
            {
                S_InitJoint NewJoint = new S_InitJoint();
                NewJoint.Load(MemStream);
                InitJoints[i] = NewJoint;
            }

            uint NumHashes = MemStream.ReadUInt32();
            ulong[] Hashes = new ulong[NumHashes];
            ushort[] Index = new ushort[NumHashes];
            for (int i = 0; i < Hashes.Length; i++)
            {
                Hashes[i] = MemStream.ReadUInt64();
                Index[i] = MemStream.ReadUInt16();
            }

            uint NumOwnerDeforms = MemStream.ReadUInt32();
            OwnerDeforms = new S_InitOwnerDeform[NumOwnerDeforms];
            for (int i = 0; i < OwnerDeforms.Length; i++)
            {
                S_InitOwnerDeform OwnerDeform = new S_InitOwnerDeform();
                OwnerDeform.Load(MemStream);
                OwnerDeforms[i] = OwnerDeform;
            }

            byte Byte0 = MemStream.ReadBit();
            byte Byte1 = MemStream.ReadBit();
        }
    }
}
