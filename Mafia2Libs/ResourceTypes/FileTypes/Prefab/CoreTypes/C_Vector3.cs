using BitStreams;

namespace ResourceTypes.Prefab
{
    public class C_Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public void Load(BitStream MemStream)
        {
            X = MemStream.ReadSingle();
            Y = MemStream.ReadSingle();
            Z = MemStream.ReadSingle();
        }

        public void Save(BitStream MemStream)
        {
            MemStream.WriteSingle(X);
            MemStream.WriteSingle(Y);
            MemStream.WriteSingle(Z);
        }

        public static C_Vector3 Construct(BitStream MemStream)
        {
            C_Vector3 Vector = new C_Vector3();
            Vector.Load(MemStream);
            return Vector;
        }
    }
}
