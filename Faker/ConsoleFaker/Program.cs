using FakerLib;
using System.Linq.Expressions;
using TestClasses;
namespace ConsoleFaker
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Faker faker = new Faker();
            Childe childe = faker.Create<Childe>();
            /*float a = faker.Create<float>();
            double b = faker.Create<double>();
            decimal n = faker.Create<decimal>();
            byte c = faker.Create<byte>();
            sbyte i = faker.Create<sbyte>();
            char d = faker.Create<char>();
            short e = faker.Create<short>();
            ushort j = faker.Create<ushort>();
            int k = faker.Create<int>();
            uint l = faker.Create<uint>();
            long f = faker.Create<long>();
            ulong m = faker.Create<ulong>();
            string g = faker.Create<string>();


            FakerConfig config = new FakerConfig();
            config.Add<Human, string, NameGenerator>(human => human.Name);
            config.Add<InHuman, string, NameGenerator>(inHuman => inHuman.Name);
            Faker faker2 = new Faker(config);

            Human human = faker.Create<Human>();
            Human human2 = faker2.Create<Human>();
            InHuman inHuman = faker.Create<InHuman>();
            InHuman inHuman2 = faker2.Create<InHuman>();*/


        }
    }
}