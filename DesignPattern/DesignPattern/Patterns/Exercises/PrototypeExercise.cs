using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

/*
 * Given the definition, you are to implement Line.DeepCopy() to perform a deep copy of the current Line Object
 */
namespace DesignPattern.Patterns.Exercises
{

    class PrototypeExercise
    {
        public class Point
        {
            public int X, Y;

            public Point()
            {

            }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public Point DeepCopy()
            {
                return new Point(X, Y);
            }
        }

        public class Line
        {
            public Point Start, End;

            public Line()
            {

            }

            public Line(Point start, Point end)
            {
                Start = start;
                End = end;
            }
            public Line DeepCopy()
            {
                return new Line(Start.DeepCopy(), End.DeepCopy());
            }
        }
    }
}
