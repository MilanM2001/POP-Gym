using SR57_2020_POP2021.Windows.ForAdministrator;
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

namespace SR57_2020_POP2021.Windows.ForUnregistered
{
    public partial class UnregisteredMainWindow : Window
    {
        public UnregisteredMainWindow()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            HomePageWindow homePageWindow = new HomePageWindow();
            this.Hide();
            homePageWindow.Show();
        }

        private void btnShowInstructors_Click(object sender, RoutedEventArgs e)
        {
            InstructorsForUnregistered instructorsForUnregistered = new InstructorsForUnregistered();
            instructorsForUnregistered.Show();
        }

        private void btnFitnessCentreInfo_Click(object sender, RoutedEventArgs e)
        {
            UnregisteredViewFitnessCentreWindow unregisteredViewFitnessCentreWindow = new UnregisteredViewFitnessCentreWindow();
            unregisteredViewFitnessCentreWindow.Show();
        }

    }
}
