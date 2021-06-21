using BitStreams;

namespace ResourceTypes.Prefab.CrashObject
{
    public class S_GlobalInitData
    {
        private uint PrefabVersion;

        public virtual void Load(BitStream MemStream)
        {
            // Should be 4
            PrefabVersion = MemStream.ReadUInt32();
        }

        public virtual void Save(BitStream MemStream)
        {
            // Should store 4
            MemStream.WriteUInt32(PrefabVersion);
        }
    }
}
