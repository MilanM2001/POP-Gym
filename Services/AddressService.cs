using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SR57_2020_POP2021.Entities;
using SR57_2020_POP2021.MyExceptions;
using System.Data.SqlClient;
using System.Data;

namespace SR57_2020_POP2021.Services
{
    class AddressService : IAddressService
    {
        public void DeleteAddress(int id)
        {
            Address address = Util.Instance.Addresses.ToList().Find(Address => Address.ID.Equals(id));
            if (address == null)
            {
                throw new WorkoutNotFoundException($"Address with that ID doesn't exist: {id}");
            }

            address.Active = false;
            Console.WriteLine("Successfully deleted Address with ID:" + id);
            UpdateAddress(address);
        }

        public void ReadAddress()
        {
            Util.Instance.Addresses = new ObservableCollection<Address>();

            using(SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedAddress = @"select * from Address";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedAddress, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Addresses");
                foreach (DataRow row in ds.Tables["Addresses"].Rows)
                {
                    Util.Instance.Addresses.Add(new Address
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        AddressCode = (string)row["AddressCode"],
                        Street = (string)row["Street"],
                        AddressNumber = (string)row["AddressNumber"],
                        City = (string)row["City"],
                        Country = (string)row["Country"],
                        Active = (bool)row["Active"]
                    });
                }
            }
        }

        public int SaveAddress(Object obj)
        {
            Address address = obj as Address;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Address (AddressCode, Street, AddressNumber, City, Country, Active)
                                        output inserted.id VALUES (@AddressCode, @Street, @AddressNumber, @City, @Country, @Active)";
                command.Parameters.Add(new SqlParameter("AddressCode", address.AddressCode));
                command.Parameters.Add(new SqlParameter("Street", address.Street));
                command.Parameters.Add(new SqlParameter("AddressNumber", address.AddressNumber));
                command.Parameters.Add(new SqlParameter("City", address.City));
                command.Parameters.Add(new SqlParameter("Country", address.Country));
                command.Parameters.Add(new SqlParameter("Active", address.Active));

                return (int)command.ExecuteScalar();
            }
        }

        public void UpdateAddress(object obj)
        {
            Address address = obj as Address;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedAddress = @"SELECT * from Address";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedAddress, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "Address");

                foreach (DataRow row in ds.Tables["Address"].Rows)
                {
                    if (row["AddressCode"].Equals(address.AddressCode))
                    {
                        row["AddressCode"] = address.AddressCode;
                        row["Street"] = address.Street;
                        row["AddressNumber"] = address.AddressNumber;
                        row["City"] = address.City;
                        row["Country"] = address.Country;
                        row["Active"] = address.Active;
                    }
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds.Tables["Address"]);


            }
        }

    }
}
