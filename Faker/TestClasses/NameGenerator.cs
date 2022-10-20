using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib;

namespace TestClasses
{
    public class NameGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            return true;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return "igor";
        }
    }
}
