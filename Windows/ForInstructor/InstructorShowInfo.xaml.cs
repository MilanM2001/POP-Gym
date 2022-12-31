using SR57_2020_POP2021.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class InstructorShowInfo : Window
    {

        ICollectionView view;

        public InstructorShowInfo(RegisteredUser registeredUser)
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;

            bool CustomFilter(object obj)
            {
                RegisteredUser user = obj as RegisteredUser;

                if (user.Role.Equals(ERole.Instructor) && user.Active && user.Email.Equals(registeredUser.Email))
                {
                    return true;
                }
                return false;
            }
        }

        private void UpdateView()
        {
            DGInstructors.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Users);
            DGInstructors.ItemsSource = view;
            DGInstructors.IsSynchronizedWithCurrentItem = true;

            DGInstructors.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGInstructors_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Active"))
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void EditInstructor_Click(object sender, RoutedEventArgs e)
        {
            RegisteredUser selectedInstructor = view.CurrentItem as RegisteredUser;

            RegisteredUser oldInstructor = selectedInstructor.Clone();

            InstructorEditInfo instructorEditInfo = new InstructorEditInfo(selectedInstructor, EStatus.Edit);
            this.Hide();
            if (!(bool)instructorEditInfo.ShowDialog())
            {
                int index = Util.Instance.Users.ToList().FindIndex(user => user.Email.Equals(oldInstructor.Email));
                Util.Instance.Users[index] = oldInstructor;
            }
            this.Show();

            view.Refresh();
            DGInstructors.SelectedItems.Clear();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }

    }
}
