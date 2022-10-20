using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class CustomComplexGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if ((!type.IsGenericType) && (!type.IsArray) && (!type.IsPrimitive) && (!type.IsEnum))
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if ((context.Faker.CustomProps.ContainsKey(typeToGenerate)) || (context.Faker.CustomFields.ContainsKey(typeToGenerate)))
            {
                return ConfigGenerate(typeToGenerate, context,  context.Faker.CustomFields.GetValueOrDefault(typeToGenerate),
                                                                context.Faker.CustomProps.GetValueOrDefault(typeToGenerate));
            }
            return SimpleGenerate(typeToGenerate,context);
        }
        public object SimpleGenerate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                foreach (var completeObject in context.ObjectChain)
                {
                    if (typeToGenerate == completeObject.GetType())
                    {
                        return completeObject;
                    }
                }

                var allFields = typeToGenerate.GetFields();
                var allProps = typeToGenerate.GetProperties();

                var allConstructors = typeToGenerate.GetConstructors();

                object res = null;
                try
                {
                    res = Activator.CreateInstance(typeToGenerate);
                }
                catch
                {

                }
                Array.Sort(allConstructors, new ConstructorComparer());
                Array.Reverse(allConstructors);

                context.TypesChain.Add(typeToGenerate);

                foreach (var constructor in allConstructors)
                {

                    try
                    {
                        var ConstructorParametrs = constructor.GetParameters();
                        List<object> preparParams = new List<object>();

                        foreach (var param in ConstructorParametrs)
                        {
                            if (context.TypesChain.Contains(param.ParameterType))
                            {
                                preparParams.Add(null);
                            }
                            else
                            {
                                preparParams.Add(context.Faker.Create(param.ParameterType));
                            }

                        }
                        res = constructor.Invoke(preparParams.ToArray());

                        break;
                    }
                    catch
                    {

                    }
                }

                context.TypesChain.Remove(typeToGenerate);

                if (res != null)
                {
                    context.ObjectChain.Add(res);
                    foreach (var property in allProps)
                    {
                        if ((property.SetMethod != null) && (!property.SetMethod.IsPrivate))
                        {
                            property.SetValue(res, context.Faker.Create(property.PropertyType));
                        }
                    }

                    foreach (var field in allFields)
                    {
                        field.SetValue(res, context.Faker.Create(field.FieldType));
                    }

                    context.ObjectChain.Remove(res);

                    return res;

                }
            }
            return null;
        }

        public object ConfigGenerate(Type typeToGenerate, GeneratorContext context,
                                        Dictionary<FieldInfo, IValueGenerator> customFields,
                                        Dictionary<PropertyInfo, IValueGenerator> customProps)
        {
            if (CanGenerate(typeToGenerate))
            {
                foreach (var completeObject in context.ObjectChain)
                {
                    if (typeToGenerate == completeObject.GetType())
                    {
                        return completeObject;
                    }
                }

                if(customFields == null)
                {
                    customFields = new Dictionary<FieldInfo, IValueGenerator>();
                }
                if(customProps == null)
                {
                    customProps = new Dictionary<PropertyInfo, IValueGenerator>();
                }

                

                List<string> privates = new List<string>();

                foreach (var field in customFields.Keys)
                {
                    if (field.IsPrivate)
                    {
                        privates.Add(field.Name);
                    }

                }

                foreach (var property in customProps.Keys)
                {
                    if ((property.SetMethod != null) && (!property.SetMethod.IsPublic))
                    {
                        privates.Add(property.Name);
                    }
                }

                var allFields = typeToGenerate.GetFields();
                var allProps = typeToGenerate.GetProperties();
                var allConstructors = typeToGenerate.GetConstructors();

                object res = null;
                try
                {
                    res = Activator.CreateInstance(typeToGenerate);
                }
                catch
                {

                }

                Array.Sort(allConstructors, new ConstructorComparer());
                Array.Sort(allConstructors, new ConstructorSearcher(privates.ToArray()));
                Array.Reverse(allConstructors);


                context.TypesChain.Add(typeToGenerate);

                foreach (var constructor in allConstructors)
                {

                    try
                    {
                        var ConstructorParametrs = constructor.GetParameters();
                        List<object> preparParams = new List<object>();

                        foreach (var param in ConstructorParametrs)
                        {
                            if (context.TypesChain.Contains(param.ParameterType))
                            {
                                preparParams.Add(null);
                            }
                            else
                            {
                                bool custom = false;
                                foreach(var key in customFields.Keys)
                                {
                                    if(key.Name==param.Name)
                                    {
                                        custom = true;
                                        preparParams.Add(customFields[key].Generate(key.FieldType,context));
                                        break;
                                    }
                                }
                                if(!custom)
                                {
                                    foreach (var key in customProps.Keys)
                                    {
                                        if (key.Name == param.Name)
                                        {
                                            custom = true;
                                            preparParams.Add(customProps[key].Generate(key.PropertyType, context));
                                            break;
                                        }
                                    }
                                }

                                if(!custom)
                                {
                                    preparParams.Add(context.Faker.Create(param.ParameterType));
                                }
                                
                            }

                        }
                        res = constructor.Invoke(preparParams.ToArray());

                        break;
                    }
                    catch
                    {

                    }
                }

                context.TypesChain.Remove(typeToGenerate);

                if (res != null)
                {
                    context.ObjectChain.Add(res);
                    foreach (var property in allProps)
                    {
                        if ((property.SetMethod != null) && (!property.SetMethod.IsPrivate))
                        {
                            if(customProps.ContainsKey(property))
                            {
                                property.SetValue(res, customProps[property].Generate(property.PropertyType,context));
                            }
                            else
                            {
                                property.SetValue(res, context.Faker.Create(property.PropertyType));
                            }
                            
                        }
                    }

                    foreach (var field in allFields)
                    {
                        if(customFields.ContainsKey(field))
                        {
                            field.SetValue(res, customFields[field].Generate(field.FieldType,context));
                        }
                        else
                        {
                            field.SetValue(res, context.Faker.Create(field.FieldType));
                        }
                        
                    }

                    context.ObjectChain.Remove(res);

                    return res;

                }
            }
            return null;
        }
    }
}
