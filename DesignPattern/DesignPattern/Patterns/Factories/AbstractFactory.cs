using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 *  used to give out abstract objects
 */


namespace DesignPattern.Patterns.Factories
{
    class AbstractFactory
    {
        public interface IHotDrink
        {
            void Consume();
        }
        internal class Tea : IHotDrink
        {
            public void Consume()
            {
                Console.Out.WriteLine("This tea is nice but I'd prefer it with milk.");
            }
        }
        internal class Coffee : IHotDrink
        {
            public void Consume()
            {
                Console.Out.WriteLine("This coffee is sensational!");
            }
        }

        public interface IHotDrinkFactory
        {
            IHotDrink Prepare(int amount);
        }

        internal class TeaFactory : IHotDrinkFactory
        {
            public IHotDrink Prepare(int amount)
            {
                Console.Out.WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy");
                return new Tea();// in real world you would customize tea
            }
        }

        internal class CoffeeFactory : IHotDrinkFactory
        {
            public IHotDrink Prepare(int amount)
            {
                Console.Out.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy");
                return new Coffee();// in real world you would customize coffee
            }
        }

        //Violates OCP
        /*
        public class HotDrinkMachine
        {
            public enum AvailableDrink
            {
                Coffee, Tea
            }

            private Dictionary<AvailableDrink, IHotDrinkFactory> factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();

            public HotDrinkMachine()
            {
                foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
                {
                    var factory = (IHotDrinkFactory)Activator.CreateInstance(
                        Type.GetType("DesignPattern.Patterns.Factories." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));
                    factories.Add(drink, factory);
                }
            }
            public IHotDrink MakeDrink(AvailableDrink drink, int amount)
            {
                return factories[drink].Prepare(amount);
            }
        }
        */

            // Gets around open closed principle by using Reflection(Should really use DI)
        public class HotDrinkMachine
        {
            private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();

            public HotDrinkMachine()
            {
                foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
                {
                    if (typeof(IHotDrinkFactory).IsAssignableFrom(t) &&
                        !t.IsInterface)
                    {
                        factories.Add(Tuple.Create(
                            t.Name.Replace("Factory", string.Empty), (IHotDrinkFactory)Activator.CreateInstance(t)
                            ));
                    }
                }
            }

            public IHotDrink MakeDrink()
            {
                Console.Out.WriteLine("Available drinks:");
                for (var index = 0; index < factories.Count; index++)
                {
                    var tuple = factories[index];
                    Console.Out.WriteLine($"{index}: {tuple.Item1}");
                }
                while (true)
                {
                    string s;
                    if ((s = Console.ReadLine()) != null 
                        && int.TryParse(s,out int i)
                        && i >= 0
                        && i < factories.Count)
                    {
                        Console.Out.Write("Specify amount :");
                        s = Console.ReadLine();
                        if (s != null && int.TryParse(s, out int amount)
                            && amount > 0)
                        {
                            return factories[i].Item2.Prepare(amount);
                        }
                    }
                    Console.Out.WriteLine("Incorrect input, try again!");
                }
            }
        }

       

    }
}
