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
                Type innerType = typeToGenerate.GetElementType();
                int rank = typeToGenerate.GetArrayRank();


                int[] lengths = new int[rank];
                int[] indices = new int[rank];
                for (int i = 0; i < rank; i++)
                {
                    lengths[i] = context.Random.Next(1, 20);
                    indices[i] = 0;
                }
                indices[0] = -1;
                
                var res = Array.CreateInstance(innerType,lengths);

                for(int i=0;i<res.Length;i++)
                {
                    int currRank = 0;
                    bool notOk = true;
                    while(notOk)
                    {
                        indices[currRank]++;
                        if (indices[currRank] == lengths[currRank])
                        {
                            indices[currRank] = 0;
                            currRank++;
                        }else
                        {
                            notOk = false;
                        }
                        
                    }

                    res.SetValue(context.Faker.Create(innerType), indices);

                }

                
                return res;
            }
            return null;
        }
    }
}
