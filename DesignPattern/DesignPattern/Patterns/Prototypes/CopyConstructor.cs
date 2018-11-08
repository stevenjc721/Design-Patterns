using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * CopyConstructor is a C++ term; Basically lets you specify an object to copy all the data from
 * Probably not familiar to many C#.Net devs
 * 
 */
namespace DesignPattern.Patterns.Prototypes
{
    class CopyConstructor
    {
        public class Person 
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
        }
        public class Address 
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
        }
    }
}
