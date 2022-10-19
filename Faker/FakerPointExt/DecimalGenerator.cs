using FakerLib;

namespace FakerPointExt
{
    public class DecimalGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if (type == typeof(decimal))
            {
                return true;
            }
            return false;
        }
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                return DecimalRandom(context.Random);
            }
            return null;
        }
        private int NextInt32(Random rng)
        {
            int firstBits = rng.Next(0, 1 << 4) << 28;
            int lastBits = rng.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        private decimal DecimalRandom(Random rng)
        {
            byte scale = (byte)rng.Next(29);
            bool sign = rng.Next(2) == 1;
            return new decimal(NextInt32(rng),
                               NextInt32(rng),
                               NextInt32(rng),
                               sign,
                               scale);
        }
    }
}
