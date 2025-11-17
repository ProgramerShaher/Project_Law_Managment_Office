using System.Collections.Generic;

namespace LawOfficeManagement.Core.Common
{
    public class Address // يرث من ValueObject لتجنب تكرار الكود
    {
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string PostalCode { get; private set; }

        // مُنشئ فارغ مطلوب من قبل EF Core
        private Address() { }

        public Address(string country, string city, string street, string postalCode)
        {
            Country = country;
            City = city;
            Street = street;
            PostalCode = postalCode;
        }

        // يجب عليك تجاوز Equals و GetHashCode ليعمل ككائن قيمة حقيقي
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Address)obj;
            return Country == other.Country &&
                   City == other.City &&
                   Street == other.Street &&
                   PostalCode == other.PostalCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Country, City, Street, PostalCode);
        }
    }
}
