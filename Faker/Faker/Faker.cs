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

        public Dictionary<Type, Dictionary<FieldInfo,IValueGenerator>> CustomFields;
        public Dictionary<Type, Dictionary<PropertyInfo, IValueGenerator>> CustomProps;

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

            context = new GeneratorContext(new Random(), this);
        }


        public Faker(FakerConfig config)
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
            foreach (var type in types)
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

            generators.Add(new CustomComplexGenerator());//ok
            generators.Add(new ArrayGenerator());//ok        

            ReadConfig(config);
            
            context = new GeneratorContext(new Random(), this);
        }

        private void ReadConfig(FakerConfig config)
        {
            CustomFields = new Dictionary<Type, Dictionary<FieldInfo, IValueGenerator>>();
            CustomProps = new Dictionary<Type, Dictionary<PropertyInfo, IValueGenerator>>();

            foreach(var a in config.Config)
            {
                PropertyInfo prop= a.member as PropertyInfo;
                if (prop != null)
                {
                    var current = CustomProps.GetValueOrDefault(a.generatorType);
                    if(current == null)
                    {
                        CustomProps.Add(a.configType, new Dictionary<PropertyInfo, IValueGenerator>());
                    }

                    current = CustomProps.GetValueOrDefault(a.configType);
                    try
                    {
                        current.Add(prop, (IValueGenerator)Activator.CreateInstance(a.generatorType));
                    }
                    catch
                    {

                    }
                }
                else
                {
                    FieldInfo field = a.member as FieldInfo;
                    if (field != null)
                    {
                        var current = CustomFields.GetValueOrDefault(a.configType);
                        if (current == null)
                        {
                            CustomFields.Add(a.configType, new Dictionary<FieldInfo, IValueGenerator>());
                        }

                        current = CustomFields.GetValueOrDefault(a.configType);
                       
                        try
                        {
                            current.Add(field, (IValueGenerator)Activator.CreateInstance(a.generatorType));
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }
    }
}
