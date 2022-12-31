using System;
using System.Windows;
using SR57_2020_POP2021.Entities;
using SR57_2020_POP2021.Windows.ForUnregistered;
using SR57_2020_POP2021.Windows.ForAdministrator;
using SR57_2020_POP2021.Windows.ForInstructor;
using SR57_2020_POP2021.Windows.ForAttendee;
using System.Data.SqlClient;
using System.Data;

namespace SR57_2020_POP2021.Windows
{

    public partial class HomePageWindow : Window
    {
        public HomePageWindow()
        {
            Util.Instance.ReadEntity("Administrators");
            Util.Instance.ReadEntity("Attendees");
            Util.Instance.ReadEntity("Instructors");
            Util.Instance.ReadEntity("Users");
            Util.Instance.ReadEntity("Workouts");
            Util.Instance.ReadEntity("Address");
            Util.Instance.ReadEntity("FitnessCentre");
            InitializeComponent();
        }

        private void btnUnregistered_Click(object sender, RoutedEventArgs e)
        {
            UnregisteredMainWindow unregisteredMainWindow = new UnregisteredMainWindow();
            this.Hide();
            unregisteredMainWindow.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Show();
        }

        private void btnRegister_Click(Object sender, RoutedEventArgs e)
        {
            {
                RegisteredUser newUser = new RegisteredUser();
                AddEditAttendeesWindow addEditAttendees = new AddEditAttendeesWindow(newUser);
                this.Hide();
                if (!(bool)addEditAttendees.ShowDialog())
                {

                }
                this.Show();
            }
        }
    }
}
