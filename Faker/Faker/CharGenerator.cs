using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class CharGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if (type == typeof(char))
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                return (char)context.Random.Next(char.MinValue, char.MaxValue+1);
                //return DoubleRandom(0, 10, context.Random);
            }

            return null;

        }
    }
}
