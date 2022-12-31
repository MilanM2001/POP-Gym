using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SR57_2020_POP2021.Entities;
using System.Text;
using System.Threading.Tasks;
using SR57_2020_POP2021.Windows.ForAdministrator;
using SR57_2020_POP2021.Windows.ForInstructor;
using SR57_2020_POP2021.Windows.ForAttendee;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace SR57_2020_POP2021.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            HomePageWindow homePageWindow = new HomePageWindow();
            homePageWindow.Show();
        }

        private void btnSubmit_Click(Object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Util.CONNECTION_STRING);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Role FROM users WHERE JMBG='" + txtJMBG.Text + "' and Password='" + txtPassword.Text + "' ", con);
            DataSet ds = new DataSet();
            
            sda.Fill(ds, "Users");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                String role = ds.Tables[0].Rows[0]["Role"].ToString();
                String JMBG = txtJMBG.Text;
                if (role == "Administrator")
                {
                    AdministratorMainWindow administratorMainWindow = new AdministratorMainWindow(JMBG);
                    administratorMainWindow.Show();
                    this.Close();   
                }
                else if (role == "Instructor")
                {
                    InstructorMainWindow instructorMainWindow = new InstructorMainWindow(JMBG);
                    instructorMainWindow.Show();
                    this.Close();
                }
                else if (role == "Attendee")
                {
                    AttendeeMainWindow attendeeMainWindow = new AttendeeMainWindow(JMBG);
                    attendeeMainWindow.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Invalid JMBG or Password");
            }

            con.Close();

        }

    }
}
