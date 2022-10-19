using FakerLib;


namespace FakerSignExt
{
    public class SByteGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if (type == typeof(sbyte))
            {
                return true;
            }
            return false;
        }
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                return SByteRandom(context.Random);
            }
            return null;
        }
        private sbyte SByteRandom(Random rand)
        {
            byte[] buf = new byte[1];
            rand.NextBytes(buf);
            return (sbyte)buf[0];
        }
    }
}
