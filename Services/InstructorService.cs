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
    class InstructorService : IInstructorService
    {
        public List<RegisteredUser> FindAllClients(string email)
        {
            throw new NotImplementedException();
        }

        public void ReadUsers()
        {
            Util.Instance.Instructors = new ObservableCollection<Instructor>();
            Util.Instance.Users = new ObservableCollection<RegisteredUser>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedInstructors = @"SELECT * from Users where Role like 'Instructor'";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedInstructors, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Instructors");
                foreach (DataRow row in ds.Tables["Instructors"].Rows)
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
            Instructor instructor = obj as Instructor;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Instructors (Id)
                                        output inserted.id VALUES (@Id)";

                command.Parameters.Add(new SqlParameter("Id", instructor.ID));

                return (int)command.ExecuteScalar();
            }
        }

        public void UpdateInstructor(Object obj)
        {
            Instructor instructor = obj as Instructor;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedInstructors = @"SELECT * from Instructors";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedInstructors, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "Instructors");

                foreach (DataRow row in ds.Tables["Instructors"].Rows)
                {
                    if (row["ID"].Equals(instructor.ID))
                    {
                        row["ID"] = instructor.ID;
                    }
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds.Tables["Instructors"]);


            }
        }
    }
}
