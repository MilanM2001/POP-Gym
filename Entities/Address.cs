using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR57_2020_POP2021.Entities
{
   public class Address
    {
        private int _id;
        private String addressCode;
        private String street;
        private String addressNumber;
        private String city;
        private String country;
        private bool active;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string AddressCode
        {
            get { return addressCode; }
            set { addressCode = value; }
        }

        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        public string AddressNumber
        {
            get { return addressNumber; }
            set { addressNumber = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string Country
        {
            get { return country; }
            set { country = value;  }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public Address() { }

        public override string ToString() {
            return $"{nameof(ID)}: {ID}, {nameof(AddressCode)}: {AddressCode}, {nameof(Street)}: {Street}, {nameof(AddressNumber)}: {AddressNumber}," +
                $" {nameof(City)}: {City}, {nameof(Country)}: {Country}, {nameof(Active)}: {Active}"; }

        public Address Clone()
        {
            Address copy = new Address();
            copy.ID = ID;
            copy.AddressCode = AddressCode;
            copy.Street = Street;
            copy.AddressNumber = AddressNumber;
            copy.City = City;
            copy.Country = Country;
            copy.Active = Active;

            return copy;
        }

    }
}
