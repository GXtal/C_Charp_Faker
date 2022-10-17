using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class ComplexGenerator : IValueGenerator
    {
        public bool CanGenerate(Type type)
        {
            if((!type.IsGenericType)&&(!type.IsArray) && (!type.IsPrimitive) && (!type.IsEnum))
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                foreach(var completeObject in context.ObjectChain)
                {
                    if(typeToGenerate==completeObject.GetType())
                    {
                        return completeObject;
                    }
                }
                
                var allFields=typeToGenerate.GetFields();
                var allProps=typeToGenerate.GetProperties();

                var allConstructors = typeToGenerate.GetConstructors();

                object res=null;
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
                        
                        foreach(var param in ConstructorParametrs)
                        {                            
                            if(context.TypesChain.Contains(param.ParameterType))
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

                if (res!=null)
                {
                    context.ObjectChain.Add(res);
                    foreach(var property in allProps )
                    {
                        if ((property.SetMethod != null) && (!property.SetMethod.IsPrivate))
                        {
                            property.SetValue(res, context.Faker.Create(property.PropertyType));
                        }
                    }

                    foreach(var field in allFields)
                    {
                        field.SetValue(res, context.Faker.Create(field.FieldType));
                    }

                    context.ObjectChain.Remove(res);

                    return res;                 
                    
                }
            }
            return null;
        }
    }
}
