using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
/*
 * Dependency Inversion Principle: Refers to a specific form of decoupling software modules.
 * The conventional dependency relationship established from high-level, policy-setting modules to low-level, dependency modules are reversed.
 * thus rendering high-level modules independent of the low-level module implementation details.
 * 
 * High-level modules should not depend on low-level modules. Both should depend on Abstraction.
 * Abstractions should not depend on details. Details should depend on Abstraction.
 */

namespace DesignPattern.Patterns.Principles
{
    class DependencyInversionPrinciple
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
            //public Datetime DateOfBirth;
        }
        public interface IRelationshipBrower
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }

        //low-level
        public class Relationships : IRelationshipBrower
        {
            private List<(Person, Relationship, Person)> relations
                = new List<(Person, Relationship, Person)>();

            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }
            //Example of what not to do
            // public List<(Person, Relationship, Person)> Relations => relations; // bad practice exposes low level 

                //Abstraction example of what to do
            public IEnumerable<Person> FindAllChildrenOf(string name)
            {
                return relations.Where(
                x => x.Item1.Name == name &&
                x.Item2 == Relationship.Parent
                ).Select(r => r.Item3);
            }
        }


        public class Research
        {
            /* Example of what not to do.
            public Research(Relationships relationships)
            {
                var relations = relationships.Relations;
                foreach (var r in relations.Where(
                    x => x.Item1.Name == "John" &&
                    x.Item2 == Relationship.Parent
                    ))
                {
                    WriteLine($"John has a child called {r.Item3.Name}");
                }
            }
            */

                //Example of what to do. Depend on abstraction of Interface
            public Research(IRelationshipBrower browser)
            {
                foreach (var p in browser.FindAllChildrenOf("John"))
                    WriteLine($"John has a child called {p.Name}");
            }

        }

    }
}
