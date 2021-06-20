using BitStreams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_AxleWheelInit
    {
        // TODO: Determine if NumAxle is always be Result * 2
        public void Load(BitStream MemStream)
        {
            uint NumAxles = MemStream.ReadUInt32() * 2;
            S_InitAxle[] Axles = new S_InitAxle[NumAxles];
            for (uint i = 0; i < NumAxles; i++)
            {
                S_InitAxle Axle = new S_InitAxle();
                Axle.Load(MemStream);

                Axles[i] = Axle;
            }
        }
    }
}
