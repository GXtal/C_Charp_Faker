using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class LongGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if (type == typeof(long))
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                return LongRandom(context.Random);
            }
            
            return null;

        }

        private long LongRandom(Random rand)
        {

            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return longRand;
        }
    }
}
