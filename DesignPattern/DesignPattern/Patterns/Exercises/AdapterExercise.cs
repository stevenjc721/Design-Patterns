using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Given an IRectangle interface and an extension method on it. Try to define a
 * SquareToRectangleAdapter that adapts the Square to the IRectangle interface.
 */
namespace DesignPattern.Patterns.Exercises
{

    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        private readonly Square Square;

        public SquareToRectangleAdapter(Square square)
        {
            this.Square = square;
        }

        public int Width => Square.Side;

        public int Height => Square.Side;
    }

}
