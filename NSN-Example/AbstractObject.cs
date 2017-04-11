using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSN.Example
{
    abstract class AbstractObject
    {
        public abstract double GetAverage(int a, int b);
        public abstract int GetMax(int a, int b);
        public abstract int GetMin(int a, int b);
    }
}
