using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Patterns.Exercises
{
    class BridgeExercise
    {
        public interface IRenderer
        {
            string WhatToRenderAs { get; }
        }

        public class VectorRenderer : IRenderer
        {
            public string WhatToRenderAs
            {
               get{ return "lines"; }
            }
        }

        public class RasterRenderer : IRenderer
        {
            public string WhatToRenderAs
            {
                get { return "pixels"; }
            }
        }
        public abstract class Shape
        {
            private IRenderer renderer;

            protected Shape(IRenderer renderer) {
                this.renderer = renderer;
            }

            public string Name { get; set; }

            public override string ToString()
            {
                return $"Drawing {Name} as {renderer.WhatToRenderAs}";
            }
        }

        public class Triangle : Shape
        {
            public Triangle(IRenderer strategy) : base(strategy)
            {
                Name = "Triangle";
            }
        }

        public class Square : Shape
        {
            public Square(IRenderer strategy) : base(strategy)
            {
                Name = "Square";
            }
        }
    }
}
