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
    public class UserService : IUserService
    {
        public void DeleteUser(int id)
        {
            RegisteredUser registeredUser = Util.Instance.Users.ToList().Find(User => User.ID.Equals(id));
            if(registeredUser == null)
            {
                throw new UserNotFoundException($"User with that ID doesn't exist: {id}");
            }

            registeredUser.Active = false;
            Console.WriteLine("Successfully deleted user with ID:" + id);
            UpdateUser(registeredUser);
        }

        public void ReadUsers()
        {
            Util.Instance.Users = new ObservableCollection<RegisteredUser>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedAdministrators = @"SELECT * from Users";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedAdministrators, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Users");
                foreach (DataRow row in ds.Tables["Users"].Rows)
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
            RegisteredUser user = obj as RegisteredUser;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Users (FirstName, LastName, JMBG, Gender, Address_ID, Email, Password, Role, Active)
                                        output inserted.id VALUES (@FirstName, @LastName, @JMBG, @Gender, @Address_ID, @Email, @Password, @Role, @Active)";
                command.Parameters.Add(new SqlParameter("FirstName", user.Name));
                command.Parameters.Add(new SqlParameter("LastName", user.Surname));
                command.Parameters.Add(new SqlParameter("JMBG", user.JMBG));
                command.Parameters.Add(new SqlParameter("Gender", user.Gender.ToString()));
                command.Parameters.Add(new SqlParameter("Address_ID", user.Address_ID));
                command.Parameters.Add(new SqlParameter("Email", user.Email));
                command.Parameters.Add(new SqlParameter("Password", user.Password));
                command.Parameters.Add(new SqlParameter("Role", user.Role.ToString()));
                command.Parameters.Add(new SqlParameter("Active", user.Active));

                return (int)command.ExecuteScalar();
            }
        }

        public void UpdateUser(object obj)
        {
            RegisteredUser registeredUser = obj as RegisteredUser;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedUsers = @"SELECT * from Users";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedUsers, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "Users");

                foreach (DataRow row in ds.Tables["Users"].Rows)
                {
                    if (row["JMBG"].Equals(registeredUser.JMBG))
                    {
                        row["FirstName"] = registeredUser.Name;
                        row["LastName"] = registeredUser.Surname;
                        row["JMBG"] = registeredUser.JMBG;
                        row["Gender"] = registeredUser.Gender;
                        row["Address_ID"] = registeredUser.Address_ID;
                        row["Email"] = registeredUser.Email;
                        row["Password"] = registeredUser.Password;
                        row["Role"] = registeredUser.Role;
                        row["Active"] = registeredUser.Active;
                    }
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds.Tables["Users"]);

     
            }
        }

    }
}
