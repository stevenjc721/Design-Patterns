using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Liskov Substitution Principle: States that if S is a subtype of T then objects of Type T may be replaced with objects of type S
 * without altering any of the desirable properties of the program.
 */
namespace DesignPattern.Patterns.Principles
{
    class LiskovSubstitutionPrinciple
    {
        public class Rectangle
        {
            public virtual int Width { get; set; }
            public virtual int Height { get; set; }

            public Rectangle()
            {
            }

            public Rectangle(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public override string ToString()
            {
                return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
            }
        }

        public class Square : Rectangle
        {
            public override int Width
            {
                set { base.Width = base.Height = value;}
            }

            public override int Height
            {
                set { base.Width = base.Height = value; }
            }
        }

        static public int Area(Rectangle r) => r.Width * r.Height;
    }
}
