using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * nonstatic properties refer to static fields so Singleton is a sense but also allows for construstors. But data is still shared across instances.
 */
namespace DesignPattern.Patterns.Singleton
{
    class Monostate
    {

        public class CEO
        {
            private static string name;
            private static int age;

            public string Name
            {
                get => name;
                set => name = value;
            }

            public int Age
            {
                get => age;
                set => age = value;
            }

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
            }
        }
    }
}
