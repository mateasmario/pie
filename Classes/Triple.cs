using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pie.Classes
{
    public class Triple<A, B, C>
    {
        public A a { get; set; }
        public B b { get; set; }
        public C c { get; set; }

        public Triple(A a, B b, C c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
    }
}
