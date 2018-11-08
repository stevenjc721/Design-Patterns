using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MoreLinq;
using NUnit.Framework;


namespace DesignPattern.Patterns.Singleton
{
    class SingletonImplementation
    {
        public interface IDatabase
        {
            int GetPopulation(string name);
        }        

        public class SingletonDatabase : IDatabase
        {
            private Dictionary<string, int> capitals;

            //No way to create since it is private
            private SingletonDatabase()
            {
                Console.Out.WriteLine("Initializing database");

                capitals = File.ReadAllLines("capitals.txt")
                    .Batch(2)
                    .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                    );
            }
                       
            public int GetPopulation(string name)
            {
                return capitals[name];
            }
            //Lazy way to create db
            private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

            public static SingletonDatabase Instance => instance.Value;
        }
    }
}
