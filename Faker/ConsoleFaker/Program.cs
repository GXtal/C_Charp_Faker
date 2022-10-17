using FakerLib;
using TestClasses;
namespace ConsoleFaker
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Faker faker = new Faker();
            /*double a = faker.Create<double>();
            byte b = faker.Create<byte>();
            char c = faker.Create<char>();
            short s = faker.Create<short>();
            long l = faker.Create<long>();
            string s2 = faker.Create<string>();*/

            var a = faker.Create<Human>();
        }
    }
}