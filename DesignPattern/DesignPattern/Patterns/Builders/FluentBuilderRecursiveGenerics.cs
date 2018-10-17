﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Patterns.Builders
{
    class FluentBuilderRecursiveGenerics
    {
        public class Person
        {
            public string Name;

            public string Position;

            public class Builder : PersonJobBuilder<Builder>
            {

            }

            public static Builder New => new Builder();

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }
        }

        public abstract class PersonBuilder
        {
            protected Person person = new Person(); // protected for Inheritance

            public Person Build()
            {
                return person;
            }
        }

        //class foo : Bar<Foo> 
        public class PersonInfoBuilder<SELF> : PersonBuilder
            where SELF: PersonInfoBuilder<SELF>
        {

            public SELF Called(string name)
            {
                person.Name = name;
                return (SELF)this;
            }
        }

        public class PersonJobBuilder<SELF> 
            : PersonInfoBuilder<PersonJobBuilder<SELF>>
            where SELF : PersonJobBuilder<SELF>
        {
            public SELF WorksAsA(string position)
            {
                person.Position = position;
                return (SELF) this;
            }
        }

    }
}
