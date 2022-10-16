using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class StringGenerator : IValueGenerator
    {
        static private char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
                                         
        public bool CanGenerate(Type type)
        {
            if (type == typeof(string))
            {
                return true;
            }
            return false;
        }

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            if (CanGenerate(typeToGenerate))
            {
                int len = context.Random.Next(1, 20);
                string word = "";
                for (int i = 0; i < len; i++)
                {                    
                    int letter_num = context.Random.Next(0, letters.Length - 1);
                    word += letters[letter_num];
                }
                return word;

            }
            return null;

        }
    }
}