using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class IntGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if (type == typeof(int))
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {

            if (CanGenerate(typeToGenerate))
            {
                return context.Random.Next();
            }
            return null;                      

        }
    }
}
