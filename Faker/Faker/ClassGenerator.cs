using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class ClassGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if((type.IsClass)&&(!type.IsGenericType))
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                typeToGenerate.GetFields();
                var allConstructors = typeToGenerate.GetConstructors();
            }
            return null;
        }
    }
}
