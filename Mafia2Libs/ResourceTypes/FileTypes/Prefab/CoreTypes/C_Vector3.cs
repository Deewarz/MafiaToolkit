using BitStreams;
using System.ComponentModel;
using Utils.Helpers.Reflection;

namespace ResourceTypes.Prefab
{
    [TypeConverter(typeof(ExpandableObjectConverter)), PropertyClassAllowReflection]
    public class C_Vector3
    {
        [PropertyForceAsAttribute]
        public float X { get; set; }
        [PropertyForceAsAttribute]
        public float Y { get; set; }
        [PropertyForceAsAttribute]
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

        public override string ToString()
        {
            return string.Format("X:{0} Y:{1} Z:{2}", X, Y, Z);
        }
    }
}
