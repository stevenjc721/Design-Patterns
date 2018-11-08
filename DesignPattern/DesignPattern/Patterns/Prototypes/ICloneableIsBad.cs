using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * ICloneable is ambiguous(could be shallow or deep clone) 
 * Array.Clone refers to a shallow clone so it isn't consistent if you force ICloneable to be a deep clone 
 */
namespace DesignPattern.Patterns.Prototypes
{
    class ICloneableIsBad
    {
        public class Person : ICloneable //never know if it is deep cloning or not ie clone new address or reference?
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

            public override string ToString()
            {
                return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}"; ;
            }

            //No specification if deep copy or shallow copy

            public object Clone()
            {
                //Doesn't really fix the problem
                //return new Person(Names, Address);

                //Is dangerous because you are returning an object
                return new Person(Names, (Address)Address.Clone());
            }
        }
        public class Address : ICloneable
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

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
            }

            public object Clone()
            {
                //Doesn't really fix the problem
                return new Address(StreetName, HouseNumber);
            }
        }
    }
}
