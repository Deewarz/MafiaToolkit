﻿using BitStreams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace ResourceTypes.Prefab.CrashObject
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class S_InitDrainEnergy
    {
        public uint DrainPart { get; set; }
        public float DrainEnergyCoeff { get; set; }

        public void Load(BitStream MemStream)
        {
            DrainPart = MemStream.ReadUInt32();
            DrainEnergyCoeff = MemStream.ReadInt32();
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt32(DrainPart);
            MemStream.WriteSingle(DrainEnergyCoeff);
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class S_InitInternalImpulse
    {
        public C_Vector3 Direction { get; set; }
        public C_Vector3 DirectioNormal { get; set; }
        public C_Vector3 Position { get; set; }
        public float Gain { get; set; }
        public float GainSpreadDown { get; set; }
        public float DirectionSpread { get; set; }
        public float VersionOrEnergy { get; set; }
        public float Flags { get; set; }

        public S_InitInternalImpulse()
        {
            Direction = new C_Vector3();
            DirectioNormal = new C_Vector3();
            Position = new C_Vector3();
        }

        public void Load(BitStream MemStream)
        {
            Direction.Load(MemStream);
            DirectioNormal.Load(MemStream);
            Position.Load(MemStream);
            Gain = MemStream.ReadSingle();
            GainSpreadDown = MemStream.ReadSingle();
            DirectionSpread = MemStream.ReadSingle();
            VersionOrEnergy = MemStream.ReadSingle();
            Flags = MemStream.ReadSingle();
        }

        public void Save(BitStream MemStream)
        {
            Direction.Save(MemStream);
            DirectioNormal.Save(MemStream);
            Position.Save(MemStream);
            MemStream.WriteSingle(Gain);
            MemStream.WriteSingle(GainSpreadDown);
            MemStream.WriteSingle(DirectionSpread);
            MemStream.WriteSingle(VersionOrEnergy);
            MemStream.WriteSingle(Flags);
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class S_InitDeformOrigData
    {
        public ulong Hash { get; set; }
        public C_Transform Unk1 { get; set; }
        public ushort[] Unk2 { get; set; }
        public ushort Unk3 { get; set; }
        public ushort Unk4 { get; set; }

        public S_InitDeformOrigData()
        {
            Unk1 = new C_Transform();
        }

        public void Load(BitStream MemStream)
        {
            Hash = MemStream.ReadUInt64();
            Unk1.Load(MemStream);

            // Read array
            uint NumUnk2 = MemStream.ReadUInt32();
            Unk2 = new ushort[NumUnk2];
            for(int i = 0; i < NumUnk2; i++)
            {
                Unk2[i] = MemStream.ReadUInt16();
            }

            Unk3 = MemStream.ReadUInt16();
            Unk4 = MemStream.ReadUInt16();
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt64(Hash);
            Unk1.Save(MemStream);

            // Write array
            MemStream.WriteInt32(Unk2.Length);
            foreach(ushort Value in Unk2)
            {
                MemStream.WriteUInt16(Value);
            }

            MemStream.WriteUInt16(Unk3);
            MemStream.WriteUInt16(Unk4);
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class S_InitDeformRelData
    { 
        public C_Vector3 Unk0 { get; set; }
        public C_Transform Transform { get; set; }

        public S_InitDeformRelData()
        {
            Unk0 = new C_Vector3();
            Transform = new C_Transform();
        }

        public void Load(BitStream MemStream)
        {
            Unk0.Load(MemStream);
            Transform.Load(MemStream);
        }

        public void Save(BitStream MemStream)
        {
            Unk0.Save(MemStream);
            Transform.Save(MemStream);
        }
    }


    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class S_InitDeformPart
    {
        public uint Unk0 { get; set; }
        public uint Unk1 { get; set; }
        public byte Unk2 { get; set; }
        public ulong[] Unk3 { get; set; }
        public float Unk4 { get; set; }
        public float Unk5 { get; set; }
        public float Unk6 { get; set; }
        public float Unk7 { get; set; }
        public float Unk8 { get; set; }
        public float Unk9 { get; set; }
        public S_InitInternalImpulse[] DPInternalImpulses { get; set; }
        public uint Unk11 { get; set; }
        public S_InitDrainEnergy[] DPDrainEnergy { get; set; }
        public uint Unk13 { get; set; }
        public List<S_InitCollVolume[]> CollisionVolumes { get; set; }
        public S_InitSMDeformBone[] SMDeformBones { get; set; }
        public ushort[] Unk14 { get; set; } // indexes?
        public C_Transform Unk15 { get; set; } // transform?
        public ulong Unk16 { get; set; }
        public ushort Unk17 { get; set; }
        public byte Unk18 { get; set; }
        public byte Unk19 { get; set; }
        public ushort[] Unk20 { get; set; } // indexes?
        public byte Unk21 { get; set; } // flag to check whether some data is available
        public S_InitDeformOrigData Unk21Data { get; set; } // Only present if Unk21 is valid
        public byte Unk22 { get; set; } // flag to check whether some data is available
        public S_InitDeformRelData Unk22_RelData { get; set; } // Only present if Unk22 is valid.
        public uint Unk23 { get; set; }
        public uint Unk24 { get; set; }
        public S_InitDeformPartCommon CommonData { get; set; }

        public S_InitDeformPart()
        {
            Unk3 = new ulong[0];
            DPDrainEnergy = new S_InitDrainEnergy[0];
            CollisionVolumes = new List<S_InitCollVolume[]>();
            SMDeformBones = new S_InitSMDeformBone[0];
            DPInternalImpulses = new S_InitInternalImpulse[0];
            Unk14 = new ushort[0];
            Unk15 = new C_Transform();
            Unk20 = new ushort[0];
            Unk21Data = new S_InitDeformOrigData();
            CommonData = new S_InitDeformPartCommon();
        }

        public void Load(BitStream MemStream)
        {
            Unk0 = MemStream.ReadUInt32();
            Unk1 = MemStream.ReadUInt32();
            Unk2 = MemStream.ReadBit();

            // collect hashes
            Unk3 = PrefabUtils.ReadHashArray(MemStream);

            Unk4 = MemStream.ReadSingle();
            Unk5 = MemStream.ReadSingle();
            Unk6 = MemStream.ReadSingle();
            Unk7 = MemStream.ReadSingle();
            Unk8 = MemStream.ReadSingle();
            Unk9 = MemStream.ReadSingle();

            uint NumDPInternalImpulses = MemStream.ReadUInt32();
            DPInternalImpulses = new S_InitInternalImpulse[NumDPInternalImpulses];
            for (int i = 0; i < NumDPInternalImpulses; i++)
            {
                S_InitInternalImpulse Packet = new S_InitInternalImpulse();
                Packet.Load(MemStream);
                DPInternalImpulses[i] = Packet;
            }

            Unk11 = MemStream.ReadUInt32(); // Count
            Debug.Assert(Unk11 == 0, "We expect one here. This has extra data!");

            // Read InitDrainEnergy data
            uint NumInitDrainEnergy = MemStream.ReadUInt32(); // Count
            DPDrainEnergy = new S_InitDrainEnergy[NumInitDrainEnergy];
            for (int i = 0; i < NumInitDrainEnergy; i++)
            {
                S_InitDrainEnergy DrainEnergyEntry = new S_InitDrainEnergy();
                DrainEnergyEntry.Load(MemStream);
                DPDrainEnergy[i] = DrainEnergyEntry;
            }

            uint NumCollisionArrays = MemStream.ReadUInt32();
            for (uint i = 0; i < NumCollisionArrays; i++)
            {
                // Read collision volumes
                uint NumCollisionVolumes = MemStream.ReadUInt32(); // Count
                S_InitCollVolume[] CollisionArray = new S_InitCollVolume[NumCollisionVolumes];
                for (int x = 0; x < NumCollisionVolumes; x++)
                {
                    S_InitCollVolume CollisionVolume = new S_InitCollVolume();
                    CollisionVolume.Load(MemStream);
                    CollisionArray[x] = CollisionVolume;
                }

                CollisionVolumes.Add(CollisionArray);
            }

            // Read SM Deform bones
            uint NumSMDeformBones = MemStream.ReadUInt32();
            SMDeformBones = new S_InitSMDeformBone[NumSMDeformBones];
            for (int i = 0; i < NumSMDeformBones; i++)
            {
                S_InitSMDeformBone DeformBone = new S_InitSMDeformBone();
                DeformBone.Load(MemStream);
                SMDeformBones[i] = DeformBone;
            }

            // UInt16s - indexes?
            uint NumIndexes = MemStream.ReadUInt32();
            Unk14 = new ushort[NumIndexes];

            for (int i = 0; i < NumIndexes; i++)
            {
                Unk14[i] = MemStream.ReadUInt16();
            }

            // Read matrix
            Unk15.Load(MemStream);

            Unk16 = MemStream.ReadUInt64();
            Unk17 = MemStream.ReadUInt16();
            Unk18 = MemStream.ReadByte();
            Unk19 = MemStream.ReadByte();

            // UInt16s - indexes?
            uint NumIndexes2 = MemStream.ReadUInt32();
            Unk20 = new ushort[NumIndexes2];
            for (int i = 0; i < NumIndexes2; i++)
            {
                Unk20[i] = MemStream.ReadUInt16();
            }

            // If one - means something is available.
            Unk21 = MemStream.ReadBit();
            if(Unk21 == 1)
            {
                Unk21Data = new S_InitDeformOrigData();
                Unk21Data.Load(MemStream);
            }

            // If one - means something is available.
            Unk22 = MemStream.ReadBit();
            if(Unk22 == 1)
            {
                Unk22_RelData = new S_InitDeformRelData();
                Unk22_RelData.Load(MemStream);
            }

            Unk23 = MemStream.ReadUInt32(); // 0?
            Unk24 = MemStream.ReadUInt32(); // 0?

            CommonData = new S_InitDeformPartCommon();
            CommonData.Load(MemStream);
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteUInt32(Unk0);
            MemStream.WriteUInt32(Unk1);
            MemStream.WriteBit(Unk2);

            PrefabUtils.WriteHashArray(MemStream, Unk3);

            MemStream.WriteSingle(Unk4);
            MemStream.WriteSingle(Unk5);
            MemStream.WriteSingle(Unk6);
            MemStream.WriteSingle(Unk7);
            MemStream.WriteSingle(Unk8);
            MemStream.WriteSingle(Unk9);

            MemStream.WriteUInt32((uint)DPInternalImpulses.Length);
            foreach (S_InitInternalImpulse Value in DPInternalImpulses)
            {
                Value.Save(MemStream);
            }

            MemStream.WriteUInt32(Unk11);

            // Write S_InitDrainEnergy entries
            MemStream.WriteUInt32((uint)DPDrainEnergy.Length);
            foreach(S_InitDrainEnergy Value in DPDrainEnergy)
            {
                Value.Save(MemStream);
            }
            
            // Write Collision Volumes
            MemStream.WriteUInt32((uint)CollisionVolumes.Count);
            foreach (S_InitCollVolume[] Arrays in CollisionVolumes)
            {
                MemStream.WriteUInt32((uint)Arrays.Length);
                foreach (S_InitCollVolume Value in Arrays)
                {
                    Value.Save(MemStream);
                }
            }

            // Write SM Deform bones
            MemStream.WriteUInt32((uint)SMDeformBones.Length);
            foreach (S_InitSMDeformBone Value in SMDeformBones)
            {
                Value.Save(MemStream);
            }

            // UInt16s - indexes?
            MemStream.WriteUInt32((uint)Unk14.Length);
            foreach (ushort Value in Unk14)
            {
                MemStream.WriteUInt16(Value);
            }

            // Write Matrix
            Unk15.Save(MemStream);

            MemStream.WriteUInt64(Unk16);
            MemStream.WriteUInt16(Unk17);
            MemStream.WriteByte(Unk18);
            MemStream.WriteByte(Unk19);

            // UInt20s - indexes?
            MemStream.WriteUInt32((uint)Unk20.Length);
            foreach (ushort Value in Unk20)
            {
                MemStream.WriteUInt16(Value);
            }

            // Write unknown hash
            MemStream.WriteBit(Unk21);
            if(Unk21 == 1)
            {
                Unk21Data.Save(MemStream);
            }

            MemStream.WriteBit(Unk22);
            if(Unk22 == 1)
            {
                Unk22_RelData.Save(MemStream);
            }

            MemStream.WriteUInt32(Unk23);
            MemStream.WriteUInt32(Unk24);

            CommonData.Save(MemStream);
        }
    }
}
