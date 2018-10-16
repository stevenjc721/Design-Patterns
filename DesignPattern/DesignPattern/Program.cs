using DesignPattern.Patterns.Principles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPattern
{
    class Program
    {
        // Single Responsibility Principle Example
        public static void SRP() {
            
            var j = new SingleResponsibilityPrinciple.Journal();

            j.AddEntry("I smile today");
            j.AddEntry("I ate a bug");
            WriteLine(j);

            var p = new SingleResponsibilityPrinciple.Persistence();
            var filename = @"c:\temp\journal.txt";
            p.SaveToFile(j, filename, true); 
            Process.Start(filename);            
        }

        // Open-Closed Principle Example
        public static void OCP() {

            var apple = new OpenClosedPrinciple.Product("Apple", OpenClosedPrinciple.Color.Green, OpenClosedPrinciple.Size.Small);
            var tree = new OpenClosedPrinciple.Product("Tree", OpenClosedPrinciple.Color.Green, OpenClosedPrinciple.Size.Large);
            var house = new OpenClosedPrinciple.Product("House", OpenClosedPrinciple.Color.Blue, OpenClosedPrinciple.Size.Large);

            OpenClosedPrinciple.Product[] products = { apple, tree, house };

            var pf = new OpenClosedPrinciple.ProductFilter();
            WriteLine("Green products (old): ");
            foreach (var p in pf.FilterByColor(products, OpenClosedPrinciple.Color.Green))
            {
                WriteLine($" - {p.Name} is green");
            }

            // ^^ BEFORE

            // vv AFTER

            var bf = new OpenClosedPrinciple.BetterFilter();
            WriteLine("Green products (new): ");
            foreach (var p in bf.Filter(products, new OpenClosedPrinciple.ColorSpecification(OpenClosedPrinciple.Color.Green)))
            {
                WriteLine($" - {p.Name} is green");
            }

            WriteLine("Large blue items: ");
            foreach (var p in bf.Filter(
                                 products,
                                 new OpenClosedPrinciple.AndSpecification<OpenClosedPrinciple.Product>(
                                     new OpenClosedPrinciple.ColorSpecification(OpenClosedPrinciple.Color.Blue),
                                     new OpenClosedPrinciple.SizeSpecification(OpenClosedPrinciple.Size.Large)
                                     )))
            {
                WriteLine($" - {p.Name} is big and blue");
            }
        }

        // Lsikov substituition Principle Example
        public static void LSP()
        {
            LiskovSubstitutionPrinciple.Rectangle rc = new LiskovSubstitutionPrinciple.Rectangle(2,3);

            WriteLine($"{rc} has area {LiskovSubstitutionPrinciple.Area(rc)}");

            LiskovSubstitutionPrinciple.Square sq = new LiskovSubstitutionPrinciple.Square();
            sq.Width = 4;
            WriteLine($"{sq} has area {LiskovSubstitutionPrinciple.Area(sq)}");

            //Cause error if setters aren't override
            LiskovSubstitutionPrinciple.Rectangle sq2 = new LiskovSubstitutionPrinciple.Square();
            sq.Width = 4;
            WriteLine($"{sq2} has area {LiskovSubstitutionPrinciple.Area(sq2)}");

        }

        // Interface Segregation Principle Example
        public static void ISP()
        {


        }


        static void Main(string[] args)
        {
            // Single Responsibility Principle Example
            // SRP();

            // Open-Closed Principle Example
            // OCP();

            // Lsikov substituition Principle Example
            // LSP();

            // Interface Segregation Principle
            ISP();

            ReadLine();
        }
    }
}
