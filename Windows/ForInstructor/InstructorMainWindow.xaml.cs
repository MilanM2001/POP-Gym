using SR57_2020_POP2021.Entities;
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
using System.Windows.Shapes;

namespace SR57_2020_POP2021.Windows.ForInstructor
{

    public partial class InstructorMainWindow : Window
    {

        private RegisteredUser registeredUser;

        public InstructorMainWindow(String JMBG)
        {
            registeredUser = Util.Instance.FindUser(JMBG);
            Title = "Instructor: " + registeredUser.Name + " " + registeredUser.Surname;
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            HomePageWindow homePageWindow = new HomePageWindow();
            this.Close();
            homePageWindow.Show();
        }

        private void btnShowPersonalInfo_Click(object sender, RoutedEventArgs e)
        {
            InstructorShowInfo instructorShowInfo = new InstructorShowInfo(registeredUser);
            instructorShowInfo.Show();
        }

        private void btnInstructorsWorkouts_Click(object sender, RoutedEventArgs e)
        {
            InstructorsWorkouts instructorsWorkouts = new InstructorsWorkouts(registeredUser);
            instructorsWorkouts.Show();
        }

        private void btnShowInstructors_Click(object sender, RoutedEventArgs e)
        {
            InstructorsForInstructor instructorsForInstructor = new InstructorsForInstructor();
            instructorsForInstructor.Show();
        }

    }
}
