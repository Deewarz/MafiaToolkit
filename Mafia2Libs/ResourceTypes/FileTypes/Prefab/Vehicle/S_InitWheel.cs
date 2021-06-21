using BitStreams;

namespace ResourceTypes.Prefab.Vehicle
{
    public class S_InitWheel
    {
        public float DeformAngleMax { get; set; }
        public float DeformEnergyMax { get; set; }
        public int[] PosLocalOrigMtr { get; set; }
        public float ArmLength { get; set; }
        public C_Vector3 WheelPosOnBrakeDrum { get; set; }
        public float BrakeDrumRadius { get; set; }
        public float BrakeDrumWidth { get; set; }
        public float BrakeDrumMass { get; set; }
        public float BrakeDrumInteria { get; set; }
        public float AxleMass { get; set; }

        public void Load(BitStream MemStream)
        {
            DeformAngleMax = MemStream.ReadSingle();
            DeformEnergyMax = MemStream.ReadSingle();

            // TODO: Check this
            uint NumArms = MemStream.ReadUInt32(); // Could be count
            if(NumArms > 0)
            {
                int z = 0;
            }

            PosLocalOrigMtr = new int[12];
            for(uint i = 0; i < PosLocalOrigMtr.Length; i++)
            {
                PosLocalOrigMtr[i] = MemStream.ReadInt32();
            }

            ArmLength = MemStream.ReadSingle();
            WheelPosOnBrakeDrum = C_Vector3.Construct(MemStream);

            BrakeDrumRadius = MemStream.ReadSingle();
            BrakeDrumWidth = MemStream.ReadSingle();
            BrakeDrumMass = MemStream.ReadSingle();
            BrakeDrumInteria = MemStream.ReadSingle();
            AxleMass = MemStream.ReadSingle();
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteSingle(DeformAngleMax);
            MemStream.WriteSingle(DeformEnergyMax);

            // TODO: Handle S_InitArm
            MemStream.WriteUInt32(0);

            // Write Matrix
            foreach (int Value in PosLocalOrigMtr)
            {
                MemStream.WriteInt32(Value);
            }

            MemStream.WriteSingle(ArmLength);
            WheelPosOnBrakeDrum.Save(MemStream);

            MemStream.WriteSingle(BrakeDrumRadius);
            MemStream.WriteSingle(BrakeDrumWidth);
            MemStream.WriteSingle(BrakeDrumMass);
            MemStream.WriteSingle(BrakeDrumInteria);
            MemStream.WriteSingle(AxleMass);
        }
    }
}
