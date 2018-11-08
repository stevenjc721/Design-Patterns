using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Given a class called Person. Person has two fields: Id & Name.
 * Implement a non-static PersonFactory that has a CreatePerson() method that takes a person's name.
 * the Id of the person should be set as a 0-based index of the object created. So, the first person the factory makes should have Id=0, second Id=1, etc...
 */
namespace DesignPattern.Patterns.Exercises
{
    class FactoryExercise
    {
        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public interface IPerson
        {
            Person CreatePerson(string Name);
        }
        public class PersonFactory : IPerson
        {
            static int id = 0;
            public Person CreatePerson(string name)
            {
                Person p = new Person();
                p.Id = id++;
                p.Name = name;

                return p;
            }
        }
    }
}
