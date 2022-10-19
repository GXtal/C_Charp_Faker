using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class Faker
    {
        private List<IValueGenerator> generators;
        private GeneratorContext context;


        private object? GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);                
            else
                return null;
        }
        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        public object? Create(Type t)
        {
            foreach(var generator in generators)
            {
                if (generator.CanGenerate(t))
                {
                    return generator.Generate(t, context);
                }
            }
            return GetDefaultValue(t);
        }
        public Faker()
        {
            generators = new List<IValueGenerator>();
            generators.Add(new ByteGenerator());//ok
            generators.Add(new ShortGenerator());//ok
            generators.Add(new IntGenerator());//ok
            generators.Add(new LongGenerator());//ok
            generators.Add(new BoolGenerator());//ok
            generators.Add(new DoubleGenerator());//ok
            generators.Add(new CharGenerator());//ok

            List<Assembly> a = new List<Assembly>();
            a.Add(Assembly.LoadFile(AppContext.BaseDirectory + "FakerSignExt.dll"));
            a.Add(Assembly.LoadFile(AppContext.BaseDirectory + "FakerPointExt.dll"));

            var types = a.SelectMany(i => i.GetTypes())
                .Where(j => typeof(IValueGenerator).IsAssignableFrom(j) &&
                        !j.IsInterface)
                .ToList();
            foreach(var type in types)
            {
                try
                {
                    generators.Add((IValueGenerator)Activator.CreateInstance(type));
                }
                catch
                {

                }
                
            }

            generators.Add(new StringGenerator());//ok

            generators.Add(new ComplexGenerator());//ok
            generators.Add(new ArrayGenerator());//ok
            //generators.Add(new StructGenerator());          

            context = new GeneratorContext(new Random(), this);
        }


        /*var types = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(i => i.GetTypes())
               .Where(j => typeof(IValueGenerator).IsAssignableFrom(j) &&
                       !j.IsInterface && !Equals(j.Name, "ObjectGenerator"))
               .ToList();*/
    }
}
