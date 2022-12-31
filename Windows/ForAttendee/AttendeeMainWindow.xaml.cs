using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SR57_2020_POP2021.Entities;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace SR57_2020_POP2021.Windows.ForAttendee
{

    public partial class AttendeeMainWindow : Window
    {

        private RegisteredUser registeredUser;

        public AttendeeMainWindow(string JMBG)
        {
            registeredUser = Util.Instance.FindUser(JMBG);
            Title = "Attendee: " + registeredUser.Name + " " + registeredUser.Surname;
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            HomePageWindow homePageWindow = new HomePageWindow();
            homePageWindow.Show();
            this.Close();
        }

        private void btnShowPersonalInfo_Click(object sender, RoutedEventArgs e)
        {
            AttendeeShowInfo attendeeShowInfo = new AttendeeShowInfo(registeredUser);
            attendeeShowInfo.Show();
        }

        private void btnShowInstructors_Click(object sender, RoutedEventArgs e)
        {
            InstructorsForAttendee instructorsForAttendee = new InstructorsForAttendee();
            instructorsForAttendee.Show();
        }

        private void btnShowMyWorkouts_Click(object sender, RoutedEventArgs e)
        {
            AttendeesWorkouts attendeesWorkouts = new AttendeesWorkouts(registeredUser);
            attendeesWorkouts.Show();
        }

    }
}
