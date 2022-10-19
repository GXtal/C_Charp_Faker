using FakerLib;

namespace FakerSignExt
{
    internal class ULongGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if (type == typeof(ulong))
            {
                return true;
            }
            return false;
        }
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                return ULongRandom(context.Random);
            }
            return null;
        }
        private ulong ULongRandom(Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            return BitConverter.ToUInt64(buf, 0);
        }
    }
}
