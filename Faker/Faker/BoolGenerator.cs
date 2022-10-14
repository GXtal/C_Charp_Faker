using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class BoolGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if (type == typeof(bool))
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                if(context.Random.Next(0,2)==1)
                {
                    return true;
                }
                return false;
            }
            return null;
        }
    }
}
