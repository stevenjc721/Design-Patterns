using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Patterns.Builders
{
    class FacetedBuilder
    {
        public class Person
        {
            //address
            public string StreetAddress, Postcode, City;

            //employment
            public string CompanyName, Position;
            public int AnnualIncome;

            public override string ToString()
            {
                return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
            }
        }

        public class PersonBuilder //Facade
        {
            //reference!
            protected Person person = new Person();

            public PersonJobBuilder Works => new PersonJobBuilder(person);
            public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

            //Not best practice
            public static implicit operator Person(PersonBuilder pb)
            {
                return pb.person;
            }
        }        

        public class PersonJobBuilder : PersonBuilder
        {
            public PersonJobBuilder(Person person)
            {
                this.person = person;
            }

            public PersonJobBuilder At(string companyName)
            {
                person.CompanyName = companyName;
                return this;
            }
            public PersonJobBuilder AsA(string position)
            {
                person.Position = position;
                return this;
            }
            public PersonJobBuilder Earning(int amount)
            {
                person.AnnualIncome = amount;
                return this;
            }
        }

        public class PersonAddressBuilder : PersonBuilder
        {
            //Might not work with a value type!
            public PersonAddressBuilder(Person person)
            {
                this.person = person;
            }

            public PersonAddressBuilder At(string streetAddress)
            {
                person.StreetAddress = streetAddress;
                return this;
            }

            public PersonAddressBuilder WithPostcode(string postcode)
            {
                person.Postcode = postcode;
                return this;
            }

            public PersonAddressBuilder In(string city)
            {
                person.City = city;
                return this;
            }
        }
    }
}
