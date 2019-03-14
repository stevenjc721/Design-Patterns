using DesignPattern.Patterns.Principles;
using DesignPattern.Patterns.Builders;
using DesignPattern.Patterns.Factories;
using DesignPattern.Patterns.Prototypes;
using DesignPattern.Patterns.Singleton;
using DesignPattern.Patterns.Adapters;
using DesignPattern.Patterns.Bridges;
using DesignPattern.Patterns.Compositions;
using DesignPattern.Patterns.Decorator;
using DesignPattern.Patterns.Flyweight;
using DesignPattern.Patterns.Exercises;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Autofac;

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
            LiskovSubstitutionPrinciple.Rectangle rc = new LiskovSubstitutionPrinciple.Rectangle(2, 3);

            WriteLine($"{rc} has area {LiskovSubstitutionPrinciple.Area(rc)}");

            LiskovSubstitutionPrinciple.Square sq = new LiskovSubstitutionPrinciple.Square();
            sq.Width = 4;
            WriteLine($"{sq} has area {LiskovSubstitutionPrinciple.Area(sq)}");

            //Cause error if setters aren't override
            LiskovSubstitutionPrinciple.Rectangle sq2 = new LiskovSubstitutionPrinciple.Square();
            sq.Width = 4;
            WriteLine($"{sq2} has area {LiskovSubstitutionPrinciple.Area(sq2)}");

        }

        // Dependency Inversion Principle Example
        public static void DIP()
        {
            var parent = new DependencyInversionPrinciple.Person { Name = "John" };
            var child1 = new DependencyInversionPrinciple.Person { Name = "Chris" };
            var child2 = new DependencyInversionPrinciple.Person { Name = "Mary" };

            var relationships = new DependencyInversionPrinciple.Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new DependencyInversionPrinciple.Research(relationships);

        }

        //Builder Example
        public static void Builder()
        {
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            WriteLine(sb);

            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul>");
            WriteLine(sb);

            // ^^ Old example of builder problem

            // vv New example of builder

            var builder = new Builder.HtmlBuilder("");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");
            WriteLine(builder.ToString());
        }

        //Fluent Builder Example
        public static void FluentBuilder()
        {
            var builder = new Builder.HtmlBuilder("");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");
            WriteLine(builder.ToString());

            // ^^ Old example of builder 

            // vv New example of Fluent builder

            var fluentbuilder = new FluentBuilder.HtmlBuilder("");
            fluentbuilder.AddChildFluent("li", "hello").AddChildFluent("li", "world");
            WriteLine(fluentbuilder.ToString());
        }

        //Fluent Builder Example with recursive generics
        public static void FluentRecursiveBuilder()
        {
            //  var builder = new FluentBuilderRecursiveGenerics.PersonJobBuilder();
            //  builder.Called("Steven");// can't .WorkAsA

            var me = FluentBuilderRecursiveGenerics.Person.New
                .Called("Steven")
                .WorksAsA("Developer")
                .Build();

            WriteLine(me);

        }

        // Faceted Builder Example
        public static void FacetedBuilder()
        {
            var pb = new FacetedBuilder.PersonBuilder();
            FacetedBuilder.Person person = pb
                                            .Lives.At("123 london Road")
                                                  .In("London")
                                                  .WithPostcode("SW12AC")
                                            .Works.At("Fabrikam")
                                                  .AsA("Engineer")
                                                  .Earning(1234567);

            WriteLine(person);

        }

        // Factory Method Example
        public static void FM()
        {
            var point = FactoryMethod.Point.NewPolarPoint(1.0, Math.PI / 2);

            WriteLine(point);

        }

        // Factory Example
        public static void F()
        {
            var point = Factory.PointFactory.NewPolarPoint(1.0, Math.PI / 2);

            WriteLine(point);

        }

        // Inner Factory Example
        public static void IF()
        {
            var point = InnerFactory.Point.Factory.NewPolarPoint(1.0, Math.PI / 2);

            WriteLine(point);
            var origin = InnerFactory.Point.Origin;

        }

        // Abstract Factory Example
        public static void AF()
        {
            var machine = new AbstractFactory.HotDrinkMachine();
            //var drink = machine.MakeDrink(AbstractFactory.HotDrinkMachine.AvailableDrink.Tea, 100); // Violates OCP
            //drink.Consume();

            var drink = machine.MakeDrink();
            drink.Consume();

        }

        // ICloneable Issues Example
        public static void ICloneableIssues()
        {
            var john = new ICloneableIsBad.Person(new[] { "John", "Smith" },
                new ICloneableIsBad.Address("London Road", 123));

            var jane = john;
            jane.Names[0] = "Jane"; // Copied Reference so you changed the values at the memory location of John

            WriteLine(john);
            WriteLine(jane);

            john.Names[0] = "John";

            var jane2 = (ICloneableIsBad.Person)john.Clone();
            jane2.Address.HouseNumber = 456;

            WriteLine(john);
            WriteLine(jane2);

        }

        // CopyConstructor Example
        public static void CC()
        {
            var john = new CopyConstructor.Person(new[] { "John", "Smith" },
                new CopyConstructor.Address("London Road", 123));

            var jane = new CopyConstructor.Person(john); // uses a constructor to initialize a clone
            jane.Address.HouseNumber = 321;

            WriteLine(john);
            WriteLine(jane);
        }

        // Explicit Deep Copy Interface Example
        public static void ExplicitDCI()
        {
            var john = new ExplicitDeepCopyInterface.Person(new[] { "John", "Smith" },
                new ExplicitDeepCopyInterface.Address("London Road", 123));

            var jane = john.DeepCopy(); // uses an Interface for generics to initialize a clone
            jane.Address.HouseNumber = 321;

            WriteLine(john);
            WriteLine(jane);
        }

        // Copy Through Serialization Example
        public static void CopySerialization()
        {
            var john = new PPerson(new[] { "John", "Smith" },
                new AAddress("London Road", 123));

            var jane = john.DeepCopy(); // Extension Binary Serialization
            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 321;
            WriteLine(john);
            WriteLine(jane);
            WriteLine();

            var jane2 = jane.DeepCopyXml(); // Extension XML Serialization
            jane2.Names[0] = "Jane2";
            jane2.Address.HouseNumber = 4321;

            WriteLine(john);
            WriteLine(jane);
            WriteLine(jane2);
        }

        // Basic Singleton Implementation Example
        public static void SI()
        {
            var db = SingletonImplementation.SingletonDatabase.Instance;
            var city = "Tokyo";
            WriteLine($"{city} has population {db.GetPopulation(city)}");
        }

        // Basic Singleton Testablility Issue Example
        public static void STI()
        {

        }

        // Singleton Monostate Example
        public static void SM()
        {
            var ceo = new Monostate.CEO();
            ceo.Name = "Adam Smith";
            ceo.Age = 55;

            var ceo2 = new Monostate.CEO();

            WriteLine(ceo2);
        }

        // Adapter Demo w/o caching Example
        public static void AD()
        {
            Demo.print();
            Demo.print();
        }

        // Adapter Demo w caching Example
        public static void AC()
        {
            AdapterCaching.print();
            AdapterCaching.print();
        }

        // Bridge Example
        public static void BridgeExample()
        {
            
            //Bridge.IRenderer renderer = new Bridge.RasterRenderer();
            Bridge.IRenderer renderer = new Bridge.VectorRenderer();

            var circle = new Bridge.Circle(renderer, 5);

            circle.Draw();
            circle.Resize(2);
            circle.Draw();

            // di is better less tedious option
            var cb = new ContainerBuilder();
            cb.RegisterType<Bridge.VectorRenderer>().As<Bridge.IRenderer>()
                .SingleInstance();

            cb.Register((c, p) =>
                new Bridge.Circle(c.Resolve<Bridge.IRenderer>(),
                                p.Positional<float>(0)));
            using (var c = cb.Build())
            {
                circle = c.Resolve<Bridge.Circle>(
                    new PositionalParameter(0, 5.0f));
                circle.Draw();
                circle.Resize(2);
                circle.Draw();
            }
           
        }

        // Geo Composition Example
        public static void CompositeGeo()
        {
            var drawing = new CompositeGeo.GraphicObject { Name = "My Drawing" };
            drawing.Children.Add(new CompositeGeo.Square { Color = "Red" }); // Real world use helper methods 
            drawing.Children.Add(new CompositeGeo.Circle { Color = "Yellow" }); // Real world use helper methods 

            var group = new CompositeGeo.GraphicObject();
            group.Children.Add(new CompositeGeo.Circle { Color = "Blue" });
            group.Children.Add(new CompositeGeo.Square { Color = "Blue" });

            drawing.Children.Add(group);

            Console.Out.WriteLine(drawing);
        }

        // Neural Net Composition Example
        public static void CompositeNN()
        {
            var neuron1 = new Neuron();
            var neuron2 = new Neuron();

            neuron1.ConnectTo(neuron2);

            var layer1 = new NeuronLayer();
            var layer2 = new NeuronLayer();

            neuron1.ConnectTo(layer1);
            layer1.ConnectTo(layer2);
        }

        // Decorator Example with String Builder
        public static void DecoratorSB()
        {
            var cb = new DecoratorStringBuilder.CodeBuilder();
            cb.AppendLine("class Foo")
              .AppendLine("{")
              .AppendLine("}");
            WriteLine(cb);
        }

        // Adapter-Decorator Example with String Builder
        public static void AdapterDecorator()
        {
            var s = "hello ";
            s += "world"; // inefficient makes a new string alot of wasted memory
            WriteLine(s);

            AdapterDecorator.MyStringBuilder s2 = "hello ";
            s2 += "world";
            WriteLine(s);
        }

        // Decorator with multiple inheritance Example 
        public static void DecoratorMI()
        {
            var b = new MultipleInheritance.Bird();
            var l = new MultipleInheritance.Lizard();
            var d = new MultipleInheritance.Dragon(b, l);

            d.Weight = 123;
            d.Fly();
            d.Crawl();

        }

        // Dynamic Decorator Example 
        public static void DynamicDecoratorEx()
        {
            var square = new DynamicDecorator.Square(1.23f);
            WriteLine(square.AsString());

            var redSquare = new DynamicDecorator.ColoredShape(square, "red");
            WriteLine(redSquare.AsString());

            var redHalfTransparentSquare = new DynamicDecorator.TransparentShape(redSquare, 0.5f);
            WriteLine(redHalfTransparentSquare.AsString());

        }

        // Static Decorator Example 
        public static void StaticDecoratorEx()
        {
            /*
             * No access to fields so you can't set Square.Sides/Circle.Radius 
             */
            var redSquare = new StaticDecorator.ColoredShape<StaticDecorator.Square>("red");
            WriteLine(redSquare.AsString());

            var circle = new StaticDecorator.TransparentShape<StaticDecorator.ColoredShape<StaticDecorator.Circle>>(0.4f);
            WriteLine(circle.AsString());


        }

        // Flyweight Text Formatting Example 
        public static void FlyweightTxtEx()
        {
            /*
             * No access to fields so you can't set Square.Sides/Circle.Radius 
             */
            var ft = new FlyweightTextFormatting.FormattedText("This is a brave new world");
            ft.Capitalize(10, 15);
            WriteLine(ft);

            var bft = new FlyweightTextFormatting.BetterFormattedText("This is a brave new world");
            bft.GetRange(10, 15).Capitalize = true;
            WriteLine(bft);

        }

        static void Main(string[] args)
        {
            // Single Responsibility Principle Example
            // SRP();

            // Open-Closed Principle Example
            // OCP();

            // Lsikov substituition Principle Example
            // LSP();

            // Dependency Inversion Principle Example
            // DIP();

            // Builder Example
            // Builder();

            // Fluent Builder Example
            //  FluentBuilder();

            // Fluent Builder Example with recursive generics
            // FluentRecursiveBuilder();

            // Faceted Builder Example
            // FacetedBuilder();

            // Builder Exercise 
            // var cb = new BuilderExercise.CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            // WriteLine(cb);

            // Factory Method Example
            // FM();

            // Factory Example
            // F();

            // Inner Factory Example
            // IF();

            // Abstract Factory Example
            // AF();

            // ICloneable Issues Example
            // ICloneableIssues();

            // CopyConstructor Example
            // CC();

            // Explicit Deep Copy Interface Example
            // ExplicitDCI();

            // Copy Through Serialization Example
            // CopySerialization();

            // Basic Singleton Implementation Example
            // SI();

            // Basic Singleton Testablility Issue Example
            // STI();

            // Singleton Monostate Example
            // SM();

            // Adapter Demo Example
            // AD();

            // Adapter Demo w caching Example
            // AC();

            // Bridge Demo
            // BridgeExample();

            // Geo Composition Example
            // CompositeGeo();

            // Decorator Example with String Builder
            // DecoratorSB();

            // Adapter-Decorator Example with String Builder
            // AdapterDecorator();

            // Decorator with multiple inheritance Example 
            //  DecoratorMI();

            // Dynamic Decorator Example 
            // DynamicDecoratorEx();

            // Static Decorator Example 
            // StaticDecoratorEx();

            // Flyweight Text Formatting Example 
             FlyweightTxtEx();

            ReadLine();
        }
    }
}
