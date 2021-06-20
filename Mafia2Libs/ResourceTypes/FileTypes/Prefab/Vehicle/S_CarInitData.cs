using BitStreams;

namespace ResourceTypes.Prefab.Vehicle
{
    public class S_CarInitData : S_VehicleInitData
    {
        public S_InitSeat[] Seats { get; set; }
        public S_InitDrWheelHSnap[] DrWheelSnap { get; set; }
        public S_InitDoorInterestingPoints[] DoorInterestingPoints { get; set; }
        public S_InitClimbBox[] ClimbBoxes { get; set; }

        public override void Load(BitStream MemStream)
        {
            base.Load(MemStream);

            ulong Hash0 = MemStream.ReadUInt64();
            ulong Hash1 = MemStream.ReadUInt64();

            // Read Seats
            uint NumSeats = MemStream.ReadUInt32();
            Seats = new S_InitSeat[NumSeats];
            for (uint i = 0; i < NumSeats; i++)
            {
                S_InitSeat SeatData = new S_InitSeat();
                SeatData.Load(MemStream);
                Seats[i] = SeatData;
            }

            // TODO: What are these?
            ulong[] Hashes2 = PrefabUtils.ReadHashArray(MemStream);
            ulong[] Hashes3 = PrefabUtils.ReadHashArray(MemStream);
            ulong[] Hashes4 = PrefabUtils.ReadHashArray(MemStream);
            ulong[] Hashes5 = PrefabUtils.ReadHashArray(MemStream);
            ulong[] Hashes6 = PrefabUtils.ReadHashArray(MemStream);

            // Read DrWheelHSnap
            uint NumDrWheelHSnap = MemStream.ReadUInt32();
            DrWheelSnap = new S_InitDrWheelHSnap[NumDrWheelHSnap];
            for (uint i = 0; i < NumDrWheelHSnap; i++)
            {
                S_InitDrWheelHSnap WheelSnap = new S_InitDrWheelHSnap();
                WheelSnap.Load(MemStream);
                DrWheelSnap[i] = WheelSnap;
            }

            // TODO: What are these?
            ulong[] Hashes8 = PrefabUtils.ReadHashArray(MemStream);
            ulong[] Hashes9 = PrefabUtils.ReadHashArray(MemStream);

            // Read DoorInterestingPoints
            uint NumInterestingPoints = MemStream.ReadUInt32();
            DoorInterestingPoints = new S_InitDoorInterestingPoints[NumInterestingPoints];
            for (uint i = 0; i < NumInterestingPoints; i++)
            {
                S_InitDoorInterestingPoints DoorPoint = new S_InitDoorInterestingPoints();
                DoorPoint.Load(MemStream);
                DoorInterestingPoints[i] = DoorPoint;
            }

            // Read ClimbBoxes
            uint NumClimbBoxes = MemStream.ReadUInt32();
            ClimbBoxes = new S_InitClimbBox[NumClimbBoxes];
            for (uint i = 0; i < NumClimbBoxes; i++)
            {
                S_InitClimbBox ClimbBox = new S_InitClimbBox();
                ClimbBox.Load(MemStream);
                ClimbBoxes[i] = ClimbBox;
            }
        }
    }
}
