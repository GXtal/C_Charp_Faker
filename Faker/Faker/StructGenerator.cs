﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class StructGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if((type.IsValueType)&&(!type.IsPrimitive)&&(!type.IsEnum))
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {

                var allFields = typeToGenerate.GetFields();
                var allProps = typeToGenerate.GetProperties();

                var allConstructors = typeToGenerate.GetConstructors();

                object res;

                res = Activator.CreateInstance(typeToGenerate);

                Array.Sort(allConstructors, new ConstructorComparer());
                Array.Reverse(allConstructors);

                foreach (var constructor in allConstructors)
                {
                    try
                    {
                        var ConstructorParametrs = constructor.GetParameters();
                        List<object> preparParams = new List<object>();
                        foreach (var param in ConstructorParametrs)
                        {
                            preparParams.Add(context.Faker.Create(param.ParameterType));
                        }
                        res = constructor.Invoke(preparParams.ToArray());

                        break;
                    }
                    catch
                    {

                    }
                }


                foreach (var property in allProps)
                {
                    try
                    {
                        property.SetValue(res, context.Faker.Create(property.PropertyType));
                    }
                    catch
                    {

                    }
                }

                foreach (var field in allFields)
                {
                    try
                    {
                        field.SetValue(res, context.Faker.Create(field.FieldType));
                    }
                    catch
                    {

                    }

                }
                return res;

            }
            return null;
        }
    }
}
