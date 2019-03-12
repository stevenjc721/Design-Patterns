using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Patterns.Bridges
{
    class Bridge
    {
        public interface IRenderer {
            void RenderCircle(float radius);
        }

        public class VectorRenderer : IRenderer
        {
            public void RenderCircle(float radius)
            {
                Console.Out.WriteLine($"Drawing a circle of radius {radius}");
            }
        }

        public class RasterRenderer : IRenderer
        {
            public void RenderCircle(float radius)
            {
                Console.Out.WriteLine($"Drawing pixels for circle with radius {radius}");
            }
        }

        public abstract class Shape
        {
            protected IRenderer renderer;

            protected Shape(IRenderer renderer)
            {
                this.renderer = renderer ?? throw new ArgumentNullException(paramName: nameof(renderer));
            }

            public abstract void Draw();
            public abstract void Resize(float factor);
        }

        public class Circle : Shape
        {
            private float radius;

            public Circle(IRenderer renderer, float radius) : base(renderer)
            {
                this.radius = radius;
            }

            public override void Draw()
            {
                renderer.RenderCircle(radius);
            }

            public override void Resize(float factor)
            {
                radius *= factor;
            }
        }
    }
}
