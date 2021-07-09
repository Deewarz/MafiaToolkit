using BitStreams;
using System.Diagnostics;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_ActorDeformInitData : S_DeformationInitData
    {
        public uint Unk0 { get; set; } // Could be count
        public S_InitActionPointData[] ActionPoints { get; set; }

        public S_ActorDeformInitData() : base()
        {
            ActionPoints = new S_InitActionPointData[0];
        }

        public override void Load(BitStream MemStream)
        {
            base.Load(MemStream);

            Unk0 = MemStream.ReadUInt32();
            Debug.Assert(Unk0 == 0, "Extra data detected");

            // Load ActionPoint data
            uint NumActionPoints = MemStream.ReadUInt32();
            ActionPoints = new S_InitActionPointData[NumActionPoints];
            for(int i = 0; i < NumActionPoints; i++)
            {
                S_InitActionPointData ActionPoint = new S_InitActionPointData();
                ActionPoint.Load(MemStream);
                ActionPoints[i] = ActionPoint;
            }
        }

        public override void Save(BitStream MemStream)
        {
            base.Save(MemStream);

            MemStream.WriteUInt32(Unk0);

            // Save ActionPoint Data
            MemStream.WriteUInt32((uint)ActionPoints.Length);
            foreach(S_InitActionPointData ActionPoint in ActionPoints)
            {
                ActionPoint.Save(MemStream);
            }
        }
    }
}
