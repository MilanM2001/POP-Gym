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
    class AttendeeService : IAttendeeService
    {
        public List<RegisteredUser> FindAllClients(string email)
        {
            throw new NotImplementedException();
        }

        public void ReadUsers()
        {
            Util.Instance.Attendees = new ObservableCollection<Attendee>();
            Util.Instance.Users = new ObservableCollection<RegisteredUser>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedAttendees = @"SELECT * from Users where Role like 'Attendee'";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedAttendees, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Attendees");
                foreach (DataRow row in ds.Tables["Attendees"].Rows)
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
            Attendee attendee = obj as Attendee;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Attendees (Id)
                                        output inserted.id VALUES (@Id)";

                command.Parameters.Add(new SqlParameter("Id", attendee.ID));

                return (int)command.ExecuteScalar();
            }
        }

        public void UpdateAttendee(Object obj)
        {
            Attendee attendee = obj as Attendee;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedAttendees = @"SELECT * from Attendees";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedAttendees, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "Attendees");

                foreach (DataRow row in ds.Tables["Attendees"].Rows)
                {
                    if (row["ID"].Equals(attendee.ID))
                    {
                        row["ID"] = attendee.ID;
                    }
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds.Tables["Attendees"]);


            }
        }
    }
}
