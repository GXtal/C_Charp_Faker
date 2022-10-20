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
        public string Name;
        public string Description { get; set; }
        public Family MyFamily { get; set; }

        private int _nya;
        private int test { get; set; }
        

        private Human()
        {
            Age = 0;
            Name = "";
        }

        public Human(int Age)
        {
            this.Age = Age;
            Name = "";
        }
    }
}