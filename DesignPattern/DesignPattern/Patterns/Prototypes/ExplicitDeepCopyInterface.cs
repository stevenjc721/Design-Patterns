using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Patterns.Prototypes
{
    class ExplicitDeepCopyInterface
    {

        public interface IPrototype<T>
        {
            T DeepCopy();
        }
        public class Person : IPrototype<Person>
        {
            public string[] Names;
            public Address Address;

            public Person(string[] names, Address address)
            {
                if (names == null)
                {
                    throw new ArgumentNullException(paramName: nameof(names));
                }
                if (address == null)
                {
                    throw new ArgumentNullException(paramName: nameof(address));
                }
                Names = names;
                Address = address;
            }
            // Makes a copy of other my copying into self
            public Person(Person other)
            {
                if (other.Names == null)
                {
                    throw new ArgumentNullException(paramName: nameof(Names));
                }
                if (other.Address == null)
                {
                    throw new ArgumentNullException(paramName: nameof(Address));
                }
                Names = other.Names;
                Address = new Address(other.Address);
            }

            public override string ToString()
            {
                return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}"; ;
            }

            public Person DeepCopy()
            {
                return new Person(Names, Address.DeepCopy());
            }
        }
        public class Address : IPrototype<Address>
        {
            public string StreetName;
            public int HouseNumber;

            public Address(string streetName, int houseNumber)
            {
                if (streetName == null)
                {
                    throw new ArgumentNullException(paramName: nameof(streetName));
                }

                StreetName = streetName;
                HouseNumber = houseNumber;
            }

            public Address(Address other)
            {
                if (other.StreetName == null)
                {
                    throw new ArgumentNullException(paramName: nameof(StreetName));
                }

                StreetName = other.StreetName;
                HouseNumber = other.HouseNumber;
            }

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
            }

            public Address DeepCopy()
            {
                return new Address(StreetName, HouseNumber);
            }
        }
    }
}
