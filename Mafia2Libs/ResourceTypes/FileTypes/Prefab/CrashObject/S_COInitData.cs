using BitStreams;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_COInitData_Packet
    {
        public ulong Hash0 { get; set; }
        public short Unk0 { get; set; }
        public short Unk1 { get; set; }
    }
    public class S_COInitData : S_ActorDeformInitData
    {
        public ulong Hash0 { get; set; }
        public S_COInitData_Packet[] COInit_Data { get; set; }

        public S_COInitData() : base()
        {
            COInit_Data = new S_COInitData_Packet[0];
        }

        public override void Load(BitStream MemStream)
        {
            base.Load(MemStream);

            Hash0 = MemStream.ReadUInt64();

            uint HumanSupportCount = MemStream.ReadUInt32();
            Debug.Assert(HumanSupportCount == 0, "Extra data detected");

            // Read array of unknown data
            uint UnknownCount = MemStream.ReadUInt32();
            COInit_Data = new S_COInitData_Packet[UnknownCount];
            for(uint i = 0; i < COInit_Data.Length; i++)
            {
                S_COInitData_Packet DataPacket = new S_COInitData_Packet();
                DataPacket.Hash0 = MemStream.ReadUInt64();
                DataPacket.Unk0 = MemStream.ReadInt16();
                DataPacket.Unk1 = MemStream.ReadInt16();
                COInit_Data[i] = DataPacket;
            }
        }

        public override void Save(BitStream MemStream)
        {
            base.Save(MemStream);

            MemStream.WriteUInt64(Hash0);

            // Change when we know HumanSupport
            MemStream.WriteUInt32(0);

            MemStream.WriteUInt32((uint)COInit_Data.Length);
            for(uint i = 0; i < COInit_Data.Length; i++)
            {
                S_COInitData_Packet DataPacket = COInit_Data[i];
                MemStream.WriteUInt64(DataPacket.Hash0);
                MemStream.WriteInt32(DataPacket.Unk0);
                MemStream.WriteInt32(DataPacket.Unk1);
            }
        }
    }
}
