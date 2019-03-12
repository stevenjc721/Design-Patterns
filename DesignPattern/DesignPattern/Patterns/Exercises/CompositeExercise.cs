using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Consider the code. The Sum() extension method adds up all the values in a list of IValueContainer elements it gets passed. We can have a single value or a set of values.
 * Complete the implementation of the interfaces so that Sum() begins to work correctly
 */
namespace DesignPattern.Patterns.Exercises
{
    public static class ExtensionMethods2
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (var c in containers)
                foreach (var i in c)
                    result += i;
            return result;
        }
    }
    public interface IValueContainer : IEnumerable<int>
    {

    }

    public class SingleValue : IValueContainer
    {
        public int Value;
        public IEnumerator<int> GetEnumerator()
        {
            yield return Value;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {

    }
}
