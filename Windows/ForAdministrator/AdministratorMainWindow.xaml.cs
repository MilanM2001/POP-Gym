using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SR57_2020_POP2021.Entities;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SR57_2020_POP2021.Windows.ForAdministrator
{

    public partial class AdministratorMainWindow : Window
    {
        private RegisteredUser registeredUser;

        public AdministratorMainWindow(String JMBG)
        {
            registeredUser = Util.Instance.FindUser(JMBG);
            Title = "Administrator: " + registeredUser.Name + " " + registeredUser.Surname;
            InitializeComponent();
        }

        private void btnShowInstructors_Click(object sender, RoutedEventArgs e)
        {
            ShowInstructorsWindow showInstructorsWindow = new ShowInstructorsWindow();
            showInstructorsWindow.Show();
        }

        private void btnShowWorkouts_Click(object sender, RoutedEventArgs e)
        {
            ShowWorkoutsWindow showWorkoutsWindow = new ShowWorkoutsWindow();
            showWorkoutsWindow.Show();
        }

        private void btnShowAttendees_Click(object sender, RoutedEventArgs e)
        {
            ShowAttendeesWindow showAttendeesWindow = new ShowAttendeesWindow();
            showAttendeesWindow.Show();
        }

        private void btnShowAdministrators_Click(object sender, RoutedEventArgs e)
        {
            ShowAdministratorsWindow showAdministratorsWindow = new ShowAdministratorsWindow();
            showAdministratorsWindow.Show();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            HomePageWindow homePageWindow = new HomePageWindow();
            homePageWindow.Show();
            this.Close();
        }

        private void btnShowAddresses_Click(object sender, RoutedEventArgs e)
        {
            ShowAddressesWindow showAddressesWindow = new ShowAddressesWindow();
            showAddressesWindow.Show();
        }

        private void btnShowFitnessCentre_Click(object sender, RoutedEventArgs e)
        {
            ShowFitnessCentreWindow showFitnessCentreWindow = new ShowFitnessCentreWindow();
            showFitnessCentreWindow.Show();
        }

        private void btnShowPersonalInfo_Click(object sender, RoutedEventArgs e)
        {
            AdministratorShowInfo administratorShowInfo = new AdministratorShowInfo(registeredUser);
            administratorShowInfo.Show();
        }

        private void btnShowRegisteredUsers_Click(object sender, RoutedEventArgs e)
        {
            ShowRegisteredUsers showRegisteredUsers = new ShowRegisteredUsers();
            showRegisteredUsers.Show();
        }

    }
}
