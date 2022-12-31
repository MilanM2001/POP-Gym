using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SR57_2020_POP2021.Entities;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SR57_2020_POP2021.Services
{
    class AdministratorService: IAdministratorService
    {
        public List<RegisteredUser> FindAllClients(string email)
        {
            throw new NotImplementedException();
        }

        public void ReadUsers()
        {
            Util.Instance.Administrators = new ObservableCollection<Administrator>();
            Util.Instance.Users = new ObservableCollection<RegisteredUser>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedAdministrators = @"SELECT * from Users where Role like 'Administrator'";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedAdministrators, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Administrators");
                foreach (DataRow row in ds.Tables["Administrators"].Rows)
                {
                    Util.Instance.Users.Add(new RegisteredUser
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Name = (string)row["FirstName"],
                        Surname = (string)row["LastName"],
                        JMBG = (string)row["JMBG"],
                        Gender = (EGender)Enum.Parse(typeof(EGender), row["Gender"].ToString(), true),
                        Address_ID = Convert.ToInt32(row["Address_ID"]),
                        Email = (string)row["Email"],
                        Password = (string)row["Password"],
                        Role = (ERole)Enum.Parse(typeof(ERole), row["Role"].ToString(), true),
                        Active = (bool)row["Active"]
                    });

                }
            }
        }

        public int SaveUser(Object obj)
        {
            Administrator administrator = obj as Administrator;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Administrators (Id)
                                        output inserted.id VALUES (@Id)";

                command.Parameters.Add(new SqlParameter("ID", administrator.ID));

                return (int)command.ExecuteScalar();
            }
        }

        public void UpdateAdministrator(object obj)
        {
            Administrator administrator = obj as Administrator;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedAdministrators = @"SELECT * from Administrators";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedAdministrators, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "Administrators");

                foreach (DataRow row in ds.Tables["Administrators"].Rows)
                {
                    if (row["ID"].Equals(administrator.ID))
                    {
                        row["ID"] = administrator.ID;
                    }
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds.Tables["Administrators"]);


            }
        }

    }
}
