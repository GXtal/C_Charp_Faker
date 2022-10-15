namespace TestClasses
{
    public struct InHuman
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }

        public InHuman()
        {
            Name = "";
            Description = "";
            Age = 0;
        }
    }
    public class Human
    {
        public int Age;
        public int Id;
        public string Name;
        private int pass;
        public int Pass { get { return pass; } set { pass = value; } }

        public Human()
        {
            Age = 0;
            Id = 0;
            Name = "";
        }

        public Human(int age)
        {
            Age = age;
            Id = 0;
            Name = "";
        }
    }
}