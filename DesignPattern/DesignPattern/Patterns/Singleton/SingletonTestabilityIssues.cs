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

/*
 * Problem is that with Singleton you have hardcoded references everywhere.
 */
namespace DesignPattern.Patterns.Singleton
{
    class SingletonTestabilityIssues
    {
        public interface IDatabase
        {
            int GetPopulation(string name);
        }

        public class SingletonDatabase : IDatabase
        {
            private Dictionary<string, int> capitals;
            private static int instanceCount; // 0
            public static int Count => instanceCount;

            //No way to create since it is private
            private SingletonDatabase()
            {
                instanceCount++;
                Console.Out.WriteLine("Initializing database");

                capitals = File.ReadAllLines(
                    Path.Combine(
                      new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt") // Safer method to read file
                    )
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

            public class SingletonRecordFinder
            {
                public int GetTotalPopulation(IEnumerable<string> names)
                {
                    int result = 0;
                    foreach (var name in names)
                        result += SingletonDatabase.Instance.GetPopulation(name);
                    return result;
                }
            }
        }
        [TestFixture]
        public class SingletonTest
        {
            [Test]
            public void IsSingletonTest()
            {
                var db = SingletonDatabase.Instance;
                var db2 = SingletonDatabase.Instance;
                Assert.That(db, Is.SameAs(db2));
                Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
            }

            [Test]
            public void SingletonTotalPopulationTest()
            {
                var rf = new SingletonDatabase.SingletonRecordFinder();
                var names = new[] { "Seoul", "Mexico City" };
                int tp = rf.GetTotalPopulation(names);
                Assert.That(tp, Is.EqualTo(17500000 + 17400000)); // Have to manually enter values. If database is changed test will fail
            }
        }
    }
}
