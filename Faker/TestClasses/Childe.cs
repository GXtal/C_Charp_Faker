using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClasses
{
    public class Childe
    {
        public int Age;
        public string Name;
        public string Description { get; set; }
        public Group MyGroup { get; private set; }

        private int _nya;
        private int test { get; set; }

        public Childe(int Age, Group family)
        {
            this.MyGroup = family;
            this.Age = Age;
            Name = "";
        }
    }
}
