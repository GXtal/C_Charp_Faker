using FakerLib;

namespace FakerSignExt
{
    public class UIntGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if (type == typeof(uint))
            {
                return true;
            }
            return false;
        }
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                return UIntRandom(context.Random);
            }
            return null;
        }
        private uint UIntRandom(Random rand)
        {
            byte[] buf = new byte[4];
            rand.NextBytes(buf);
            return BitConverter.ToUInt32(buf, 0);
        }

    }
}