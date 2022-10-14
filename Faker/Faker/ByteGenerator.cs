using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    internal class ByteGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if (type == typeof(byte))
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                return context.Random.Next(0, Byte.MaxValue);
            }
            return null;
        }
    }
}
