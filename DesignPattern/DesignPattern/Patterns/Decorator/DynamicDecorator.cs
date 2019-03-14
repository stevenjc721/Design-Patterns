using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Patterns.Decorator
{
    class DynamicDecorator
    {
        public interface IShape
        {
            string AsString();
        }

        public class Circle : IShape
        {
            private float radius;

            public Circle(float radius)
            {
                this.radius = radius;
            }
            public void Resize(float factor)
            {
                radius *= factor;
            }

            public string AsString() => $"A circle with radius {radius}";
        }

        public class Square : IShape
        {
            private float side;

            public Square(float side)
            {
                this.side = side;
            }
            public string AsString() => $"A square with side {side}";
        }

        //Dynamic
        //Decorator that can be attached onto an object
        public class ColoredShape : IShape
        {
            private IShape shape;
            private string color;

            public ColoredShape(IShape shape, string color)
            {
                this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
                this.color = color ?? throw new ArgumentNullException(paramName: nameof(color));
            }

            public string AsString() => $"{shape.AsString()} has the color {color}";
            
        }

        //Dynamic
        //Decorator that can be attached onto an object( including another decorator could have a color ontop of a color)
        public class TransparentShape : IShape
        {
            private IShape shape;
            private float transparency;

            public TransparentShape(IShape shape, float transparency)
            {
                this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
                this.transparency = transparency;
            }

            public string AsString() => $"{shape.AsString()} has {transparency * 100.0} transparency ";

        }
    }
}
