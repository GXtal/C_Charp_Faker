using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class GeneratorContext
    {
        // Для сохранения состояния генератора псевдослучайных чисел
        // и получения различных значений при нескольких последовательных вызовах.
        public Random Random { get; }

        // Даем возможность генератору использовать все возможности Faker.
        // Необходимо для создания коллекций произвольных объектов,
        // но может быть удобно и в некоторых других случаях.
        public Faker Faker { get; }

        public List<Type> TypesChain;
        public List<object> ObjectChain;
        public GeneratorContext(Random random, Faker faker)
        {
            TypesChain = new List<Type>();
            ObjectChain = new List<object>();
            Random = random;
            Faker = faker;
        }
    }
}
