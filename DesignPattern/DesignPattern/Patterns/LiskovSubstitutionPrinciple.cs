using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Liskov Substitution Principle: You should be able to substitute a base type for a subtype
 * In this case Rectangle for Square
 */
namespace DesignPattern.Patterns
{
    public class Rectangle {
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
        //Switch new to override 
        public override int Width {
            set { base.Width = base.Height = value; }
        }
        public override int Height
        {
            set { base.Height = base.Width = value; }
        }
    }
    class LiskovSubstitutionPrinciple
    {
        static public int Area(Rectangle r) => r.Width * r.Height;
        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle();
            Console.Out.WriteLine($"{rc} has area {Area(rc)}");
            // Rectangle and Square should be interchangible but if you change the Area will not be correct
            Square sq = new Square();
            sq.Width = 4;
            Console.Out.WriteLine($"{sq} has area {Area(sq)}");
            
            //How to fix? use Override in Square and make Height/Width Virtual in Rectangle
        }
    }
}
