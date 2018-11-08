using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Factories are basically simplifies Builders. Builders are required when an object cannot be produced in one step.
 * 
 * Making external factories is problematic in C# because PointFactory can't access the private Point constructor so it has to be public.
 * this doesn't hide Point constructor so people can still access it.
 */
namespace DesignPattern.Patterns.Factories
{
    class Factory
    {
        public static class PointFactory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
        public class Point
        {
            // Factory Method -- name isn't directly tied to the containing class.
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
            private double x, y;

            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public override string ToString()
            {
                return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
            }
        }
    }
}
