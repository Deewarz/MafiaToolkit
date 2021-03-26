using BitStreams;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using ResourceTypes.Prefab.CrashObject;

namespace ResourceTypes.Prefab
{
//S_DeformationInitData
//S_VehicleInitData
//S_ActorDeformData
//S_WheelInitData
//S_PhThingActorInitData
//S_BoatInitData
//S_WagonInitData
//S_BrainInitData
//2: CarInitData
//1: VehicleInitData
//0: DeformationInitData
//3: COInitData(CrashObjectInitData?)
//4: ActorDeformInitData
//5: WheelInitData
//6: PhThingActorBaseInitData
//7: DoorInitData
//9: BoatInitData
//10: WagonInitData
//11: BrainInitData
//8: LifeInitData

    public class PrefabLoader
    {
        int sizeOfFile; //-4 bytes
        int sizeOfFile2; //-12 bytes

        public PrefabStruct[] Prefabs { get; set; }


        public PrefabLoader(FileInfo file)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(file.FullName, FileMode.Open)))
            {
                ReadFromFile(reader);
            }
        }

        public void WriteToFile(FileInfo file)
        {
            UpdatePrefabMetaInfo();

            using(BinaryWriter writer = new BinaryWriter(File.Open(file.FullName, FileMode.Create)))
            {
                WriteToFile(writer);
            }
        }

        public void ReadFromFile(BinaryReader reader)
        {
            if (!Directory.Exists("Prefabs"))
                Directory.CreateDirectory("Prefabs");

            sizeOfFile = reader.ReadInt32();
            int numPrefabs = reader.ReadInt32();
            sizeOfFile2 = reader.ReadInt32();
            Prefabs = new PrefabStruct[numPrefabs];

            for (int i = 0; i < numPrefabs; i++)
            {
                Prefabs[i] = new PrefabStruct();
                Prefabs[i].ReadFromFile(reader);
            }

            Debug.Assert(reader.BaseStream.Position == reader.BaseStream.Position, "We did not reach the end of the prefab file!");
        }

        public void WriteToFile(BinaryWriter writer)
        {
            writer.Write(sizeOfFile);
            writer.Write(Prefabs.Length);
            writer.Write(sizeOfFile2);

            foreach(var prefab in Prefabs)
            {
                prefab.WriteToFile(writer);
            }
        }

        private void UpdatePrefabMetaInfo()
        {
            // Calculate size of file
            int size = 8;

            foreach (var prefab in Prefabs)
            {
                size += prefab.GetSize();
            }

            sizeOfFile = size;
            sizeOfFile2 = size - 8;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", 1, 2);
        }

        public class PrefabStruct
        {
            public ulong Hash { get; set; }
            public string AssignedName { get; set; }
            public int PrefabType { get; set; }
            [ReadOnly(true)]
            public int Unk0 {get;set;}
            [ReadOnly(true)]
            public int PrefabSize { get; set; }

            byte[] data;

            public void ReadFromFile(BinaryReader reader)
            {
                Hash = reader.ReadUInt64();
                PrefabType = reader.ReadInt32();
                Unk0 = reader.ReadInt32();
                PrefabSize = reader.ReadInt32();

                long CurrentPosition = reader.BaseStream.Position;
                data = reader.ReadBytes(PrefabSize);

                using (BinaryWriter writer = new BinaryWriter(File.Open("Prefabs/" + Hash.ToString() + "Type_" + PrefabType + ".prefab", FileMode.Create)))
                {
                    writer.Write(data);
                }

                if (Debugger.IsAttached)
                {
                    reader.BaseStream.Position = CurrentPosition;

                    BitStream MemStream = new BitStream(reader.BaseStream);

                    S_DeformationInitData DeformationData = new S_DeformationInitData();
                    DeformationData.Load(MemStream);
                }
               


                //unk4 = reader.ReadInt32();
                //unkHashCount = reader.ReadInt32();
                //unkHashes = new ulong[unkHashCount];

                //for (int i = 0; i != unkHashes.Length; i++)
                //    unkHashes[i] = reader.ReadUInt64();

                //if(reader.ReadInt32() != 0) //should be zero?
                //    Console.WriteLine("Wasn't zero.");
            }

            public void WriteToFile(BinaryWriter writer)
            {
                writer.Write(Hash);
                writer.Write(PrefabType);
                writer.Write(Unk0);
                writer.Write(PrefabSize);
                writer.Write(data);
            }

            private void GetThreeVectors4(BitStream MemStream)
            {
                int Vec1_Comp1 = MemStream.ReadInt32();
                int Vec1_Comp2 = MemStream.ReadInt32();
                int Vec1_Comp3 = MemStream.ReadInt32();

                int Vec2_Comp1 = MemStream.ReadInt32();
                int Vec2_Comp2 = MemStream.ReadInt32();
                int Vec2_Comp3 = MemStream.ReadInt32();

                int Vec3_Comp1 = MemStream.ReadInt32();
                int Vec3_Comp2 = MemStream.ReadInt32();
                int Vec3_Comp3 = MemStream.ReadInt32();

                int Vec4_Comp1 = MemStream.ReadInt32();
                int Vec4_Comp2 = MemStream.ReadInt32();
                int Vec4_Comp3 = MemStream.ReadInt32();
            }

            public int GetSize()
            {
                return PrefabSize + 20;
            }
        }
    }
}
