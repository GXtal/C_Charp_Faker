using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class DoubleGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if (type == typeof(double))
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                return DoubleRandom(context.Random);
                //return DoubleRandom(0, 10, context.Random);
            }

            return null;

        }

        private double DoubleRandom(Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            double doubleRand = Convert.ToDouble(buf);
            return doubleRand;
        }
    }
}
