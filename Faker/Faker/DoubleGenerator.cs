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
                return DoubleRandom(double.MinValue, double.MaxValue, context.Random);
                //return DoubleRandom(0, 10, context.Random);
            }

            return null;

        }

        private double DoubleRandom(double min, double max, Random rand)
        {
            if(Double.IsInfinity(max-min))
            {
                return (rand.NextDouble()-0.5) * 2 * (max);
            }
            return rand.NextDouble() * (max - min) + min;
        }
    }
}
