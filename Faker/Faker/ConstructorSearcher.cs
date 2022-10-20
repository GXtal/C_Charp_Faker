using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class ConstructorSearcher : IComparer<ConstructorInfo>
    {
        string[] privates;
        public ConstructorSearcher(string[] privates)
        {
            this.privates = privates;
        }

        private int CountMatch(ConstructorInfo? x)
        {
            int count = 0;
            foreach(var parameter in x.GetParameters())
            {
                if (privates.Contains(parameter.Name))
                {
                    count++;
                }

            }
            return count;
        }
        public int Compare(ConstructorInfo? x, ConstructorInfo? y)
        {
            return CountMatch(x).CompareTo(CountMatch(y));
        }
    }
}
