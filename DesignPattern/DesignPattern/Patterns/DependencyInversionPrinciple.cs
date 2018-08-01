using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

/*
 * Dependency Inversion Principle
 * High level parts of a system shouldn't depend on low level parts of a system.
 * Should instead depend on abstraction.
 */
namespace DesignPattern.Patterns
{
    public enum Relationship
    {
       Parent,
       Child,
       Sibling
    }

    public class Person
    {
        public string Name;
        //public DateTime DateOfBirth;
    }
    //Abstraction to solve problem of High level (Research) accessing Low Level (Relationships) 
    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }
    //low-level
    public class Relationships : IRelationshipBrowser
    {
        // because of Interface we can change the low-level data structure if we need to without breaking the high-level code because of the abstraction
        private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

        public void AddParentandChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        // public List<(Person, Relationship, Person)> Relations => relations; // Example of what not to do
        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations.Where(
               x => x.Item1.Name == name &&
                   x.Item2 == Relationship.Parent).Select(r => r.Item3);
        }
    }
    //High-Level
    public class Research
    {
        /* Example of what not to do
        public Research(Relationships relationships)
        {
            //Accessing low level property bad idea because Relationship class can't change without breaking this class.
            var relations = relationships.Relations;
            foreach (var r in relations.Where(
                x => x.Item1.Name == "John" &&
                    x.Item2 == Relationship.Parent))
            {
                WriteLine($"John has a child called {r.Item3.Name}");
            }
        }
        */
        //Example of what to do
        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
                WriteLine($"John has a child called {p.Name}");
        }
        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationships = new Relationships();
            relationships.AddParentandChild(parent, child1);
            relationships.AddParentandChild(parent, child2);

            new Research(relationships);

        }
    }

    class DependencyInversionPrinciple
    {
    }
}
