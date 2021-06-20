using BitStreams;

namespace ResourceTypes.Prefab
{
    public static class PrefabUtils
    {
        public static ulong[] ReadHashArray(BitStream MemStream)
        {
            uint ArrayLength = MemStream.ReadUInt32();
            ulong[] HashArray = new ulong[ArrayLength];
            for(uint i = 0; i < ArrayLength; i++)
            {
                HashArray[i] = MemStream.ReadUInt64();
            }

            return HashArray;
        }
    }
}
