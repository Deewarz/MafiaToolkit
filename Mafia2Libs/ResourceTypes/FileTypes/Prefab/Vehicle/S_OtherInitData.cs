using BitStreams;

namespace ResourceTypes.Prefab.Vehicle
{
    public class S_OtherInitData
    {
        public ulong VehicleBodyName { get; set; }
        public ulong RestBoneName { get; set; }
        public ulong MotorVentilatorName { get; set; }
        public ulong[] Hashes0 { get; set; }
        public ulong[] DrivingWheels { get; set; }
        public ulong[] Hashes2 { get; set; }
        public ulong[] FuelTanks { get; set; }
        public ulong[] ExhaustEmitters { get; set; }
        public ulong[] LocalWindEmitters { get; set; }
        public C_Vector3 BoneRange { get; set; }
        public float ReduceBBoxZ { get; set; }
        public int[] Matrix { get; set; }
        public int[] Matrix1 { get; set; }
        public int[] Matrix2 { get; set; }
        public S_InitDCBData[] DCBData { get; set; }
        public S_InitWindowData[] WindowData { get; set; }
        public ulong SnowRestName { get; set; }
        public ulong HeadlightModelName { get; set; }
        public ulong BacklightModelName { get; set; }
        public ulong ToplightModelName { get; set; }

        public void Load(BitStream MemStream)
        {
            // Read initial frame links
            VehicleBodyName = MemStream.ReadUInt64();
            RestBoneName = MemStream.ReadUInt64();
            MotorVentilatorName = MemStream.ReadUInt64();

            // Read lists of hashes
            Hashes0 = PrefabUtils.ReadHashArray(MemStream);
            DrivingWheels = PrefabUtils.ReadHashArray(MemStream);
            Hashes2 = PrefabUtils.ReadHashArray(MemStream);
            FuelTanks = PrefabUtils.ReadHashArray(MemStream);
            ExhaustEmitters = PrefabUtils.ReadHashArray(MemStream);
            LocalWindEmitters = PrefabUtils.ReadHashArray(MemStream);

            BoneRange = C_Vector3.Construct(MemStream);
            ReduceBBoxZ = MemStream.ReadSingle();

            // Matrices
            Matrix = new int[12];
            for(uint i = 0; i < 12; i++) // LightMatrix[0]
            {
                Matrix[i] = MemStream.ReadInt32(); // float
            }

            Matrix1 = new int[12];
            for (uint i = 0; i < 12; i++) // LightMatrix[1]
            {
                Matrix1[i] = MemStream.ReadInt32(); // float
            }

            Matrix2 = new int[12];
            for (uint i = 0; i < 12; i++) // LightMatrix[2]
            {
                Matrix2[i] = MemStream.ReadInt32(); // float
            }

            // Read InitDCBData
            uint NumDCBDatas = MemStream.ReadUInt32();
            DCBData = new S_InitDCBData[NumDCBDatas];
            for(uint i = 0; i < DCBData.Length; i++)
            {
                S_InitDCBData NewDCBData = new S_InitDCBData();
                NewDCBData.Load(MemStream);
                DCBData[i] = NewDCBData;
            }

            // Read Windows
            uint NumWindows = MemStream.ReadUInt32();
            WindowData = new S_InitWindowData[NumWindows];
            for (uint i = 0; i < NumWindows; i++)
            {
                S_InitWindowData NewWindow = new S_InitWindowData();
                NewWindow.Load(MemStream);
                WindowData[i] = NewWindow;
            }

            // Read Other Frame Names
            SnowRestName = MemStream.ReadUInt64();
            HeadlightModelName = MemStream.ReadUInt64();
            BacklightModelName = MemStream.ReadUInt64();
            ToplightModelName = MemStream.ReadUInt64();
        }

        public void Save(BitStream MemStream)
        {
            // Write initial frame links
            MemStream.WriteUInt64(VehicleBodyName);
            MemStream.WriteUInt64(RestBoneName);
            MemStream.WriteUInt64(MotorVentilatorName);

            // Write lits of hashes
            PrefabUtils.WriteHashArray(MemStream, Hashes0);
            PrefabUtils.WriteHashArray(MemStream, DrivingWheels);
            PrefabUtils.WriteHashArray(MemStream, Hashes2);
            PrefabUtils.WriteHashArray(MemStream, FuelTanks);
            PrefabUtils.WriteHashArray(MemStream, ExhaustEmitters);
            PrefabUtils.WriteHashArray(MemStream, LocalWindEmitters);

            BoneRange.Save(MemStream);
            MemStream.WriteSingle(ReduceBBoxZ);

            // Write Light matrices
            foreach(int Value in Matrix)
            {
                MemStream.WriteInt32(Value);
            }

            foreach (int Value in Matrix1)
            {
                MemStream.WriteInt32(Value);
            }

            foreach (int Value in Matrix2)
            {
                MemStream.WriteInt32(Value);
            }

            // Write InitDCBData
            MemStream.WriteUInt32((uint)DCBData.Length);
            foreach(S_InitDCBData Data in DCBData)
            {
                Data.Save(MemStream);
            }

            // Write Windows
            MemStream.WriteUInt32((uint)WindowData.Length);
            foreach (S_InitWindowData Window in WindowData)
            {
                Window.Save(MemStream);
            }

            // Write other frame links
            MemStream.WriteUInt64(SnowRestName);
            MemStream.WriteUInt64(HeadlightModelName);
            MemStream.WriteUInt64(BacklightModelName);
            MemStream.WriteUInt64(ToplightModelName);
        }
    }
}
