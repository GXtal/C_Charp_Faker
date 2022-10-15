using FakerLib;
using TestClasses;
namespace ConsoleFaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var f = new Faker();
            int a = f.Create<int>();
            bool b = f.Create<bool>();
            f.Create<string>();
            Human human = f.Create<Human>();            
            InHuman inHuman = f.Create<InHuman>();
        }
    }
}