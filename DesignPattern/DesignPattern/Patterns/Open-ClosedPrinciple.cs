using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Patterns
{
    public enum Color
    {
        Red, Green, Blue
    }
    public enum Size
    {
        Small, Medium, Large, Huge
    }

    public class Product
    {
        // illegal in .NET bad practice should be private
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }
    }
    /*
     * Example of what not to do. 
     * ProductFilter requires modification for every new filter
     */
    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
                if (p.Size == size)
                    yield return p;
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
                if (p.Color == color)
                    yield return p;
        }

        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var p in products)
                if (p.Color == color && p.Size == size)
                    yield return p;
        }
    }

    /*
     * Example of what to do.
     * Generic, required no modification to add functionality only extension
     */
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;
        public ColorSpecification(Color color)
        {
            this.color = color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }
    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;
        public SizeSpecification(Size size)
        {
            this.size = size;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }
    public class AndSpecification<T> : ISpecification<T>
    {
        ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(paramName: nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException(paramName: nameof(second));
            }
            this.first = first;
            this.second = second;
        }
        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }

    class Open_ClosedPrinciple
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };
            //Bad Example required modification
            var pf = new ProductFilter();
            Console.Out.WriteLine("Green products (old):");
            foreach (var p in pf.FilterByColor(products, Color.Green))
            {
                Console.Out.WriteLine($" -{p.Name} is green ");
            }

            //Good Example required no modification only extension
            var bf = new BetterFilter();
            Console.Out.WriteLine("Green products(new):");
            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
               Console.Out.WriteLine($" -{p.Name} is green ");
            }

            Console.Out.WriteLine("Large blue items");
            foreach (var p in bf.Filter(products,
                new AndSpecification<Product>(new ColorSpecification(Color.Blue),
                                              new SizeSpecification(Size.Large))
                ))
            {
                Console.Out.WriteLine($" - {p.Name} is big and blue");
            }
        }
    }
}
