using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Exercise is to write a method called IsSingleton(). This method takes a factory method that returns an object
 * and it's up to you to determine whether or not that object is a singleton instance.
 */
namespace DesignPattern.Patterns.Exercises
{
    class SingletonExercise
    {
        public static bool IsSingleton(Func<object> func)
        {
            var x = func();
            var x2 = func();

            return ReferenceEquals(x, x2);
        }
    }
}
