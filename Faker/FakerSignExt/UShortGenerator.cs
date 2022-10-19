using FakerLib;

namespace FakerSignExt
{
    public class UShortGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if (type == typeof(ushort))
            {
                return true;
            }
            return false;
        }
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                return UShortRandom(context.Random);
            }
            return null;
        }
        private ushort UShortRandom(Random rand)
        {
            byte[] buf = new byte[2];
            rand.NextBytes(buf);
            return BitConverter.ToUInt16(buf, 0);
        }
    }
}
