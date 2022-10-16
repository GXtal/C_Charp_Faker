using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class ShortGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if (type == typeof(short))
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                return (short)context.Random.Next(short.MinValue,short.MaxValue+1);
            }
            return null;

        }
    }
}
