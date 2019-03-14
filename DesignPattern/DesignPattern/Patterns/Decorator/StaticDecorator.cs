using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Patterns.Decorator
{
    class StaticDecorator
    {
        public abstract class Shape
        {
            public abstract string AsString();
        }

        public class Circle : Shape
        {
            private float radius;

            public Circle() : this(0)
            {

            }
            public Circle(float radius)
            {
                this.radius = radius;
            }
            public void Resize(float factor)
            {
                radius *= factor;
            }

            public override string AsString() => $"A circle with radius {radius}";
        }

        public class Square : Shape
        {
            private float side;

            public Square() : this(0)
            {

            }
            public Square(float side)
            {
                this.side = side;
            }
            public override string AsString() => $"A square with side {side}";
        }

        // CRTP not in C#
        //public class ColoredShape<T> : T   

        //static (not great in c# much better in c++)
        public class ColoredShape<T> : Shape where T: Shape, new()
        {
            private string color;
            private T shape = new T();

            public ColoredShape() : this("black")
            {

            }
            public ColoredShape(string color)
            {
                this.color = color ?? throw new ArgumentNullException(paramName: nameof(color));
            }

            public override string AsString() => $"{shape.AsString()} has the color {color}";
        }

        //static (not great in c# much better in c++)
        public class TransparentShape<T> : Shape where T : Shape, new()
        {
            private float transparency;
            private T shape = new T();

            public TransparentShape() : this(0.0f)
            {

            }
            public TransparentShape(float transparency)
            {
                this.transparency = transparency;
            }

            public override string AsString() => $"{shape.AsString()} has {transparency * 100.0f}% transparency ";
        }
    }
}
