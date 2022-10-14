using FakerLib;
namespace ConsoleFaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var f = new Faker();
            int a = f.Create<int>();
            bool b = f.Create<bool>();
        }
    }
}