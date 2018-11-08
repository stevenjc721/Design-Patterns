using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace DesignPattern.Patterns.Prototypes
{

    public static class ExtensionMethods // Requires Serializable classes
    {
        public static T DeepCopy<T>(this T self) // Binary Serialization
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();

            return (T)copy;
        }

        //(Requires parameterless constructors)
        public static T DeepCopyXml<T>(this T self) // Xml Serialization
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;

                return (T) s.Deserialize(ms);
            }
        }
    }
    [Serializable]
    public class PPerson
    {
        public string[] Names;
        public AAddress Address;

        public PPerson()
        {
        }
            public PPerson(string[] names, AAddress address)
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
        public PPerson(PPerson other)
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
            Address = new AAddress(other.Address);
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}"; ;
        }

    }
    [Serializable]
    public class AAddress
    {
        public string StreetName;
        public int HouseNumber;

        public AAddress()
        {
        }
            public AAddress(string streetName, int houseNumber)
        {
            if (streetName == null)
            {
                throw new ArgumentNullException(paramName: nameof(streetName));
            }

            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public AAddress(AAddress other)
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
