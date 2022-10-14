using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    internal class StructGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if((type.IsValueType)&&(!type.IsPrimitive))
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            throw new NotImplementedException();
        }
    }
}
