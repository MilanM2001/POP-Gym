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

namespace SR57_2020_POP2021.Windows.ForAttendee
{
    public partial class AttendeeShowInfo : Window
    {

        ICollectionView view;

        public AttendeeShowInfo(RegisteredUser registeredUser)
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;

            bool CustomFilter(object obj)
            {
                RegisteredUser user = obj as RegisteredUser;

                if (user.Role.Equals(ERole.Attendee) && user.Active && user.Email.Equals(registeredUser.Email))
                {
                    return true;
                }
                return false;
            }
        }

        private void UpdateView()
        {
            DGAttendees.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Users);
            DGAttendees.ItemsSource = view;
            DGAttendees.IsSynchronizedWithCurrentItem = true;

            DGAttendees.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGAttendees_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Active"))
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void EditAttendee_Click(object sender, RoutedEventArgs e)
        {
            RegisteredUser selectedAttendee = view.CurrentItem as RegisteredUser;

            RegisteredUser oldAttendee = selectedAttendee.Clone();

            AttendeeEditInfo instructorEditInfo = new AttendeeEditInfo(selectedAttendee, EStatus.Edit);
            this.Hide();
            if (!(bool)instructorEditInfo.ShowDialog())
            {
                int index = Util.Instance.Users.ToList().FindIndex(user => user.Email.Equals(oldAttendee.Email));
                Util.Instance.Users[index] = oldAttendee;
            }
            this.Show();

            view.Refresh();
            DGAttendees.SelectedItems.Clear();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }

    }
}
