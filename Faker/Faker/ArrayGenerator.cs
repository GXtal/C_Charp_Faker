using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class ArrayGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if(type.IsArray)
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                
            }
            return null;
        }
    }
}
