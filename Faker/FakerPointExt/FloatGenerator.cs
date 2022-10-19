using FakerLib;
namespace FakerPointExt
{
    public class FloatGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if (type == typeof(float))
            {
                return true;
            }
            return false;
        }
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                return FloatRandom(context.Random);
            }
            return null;
        }
        private float FloatRandom(Random rand)
        {
            byte[] buf = new byte[4];
            rand.NextBytes(buf);
            return BitConverter.ToSingle(buf,0);
        }
    }
}