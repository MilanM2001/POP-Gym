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
using System.ComponentModel;
using SR57_2020_POP2021.Entities;
using SR57_2020_POP2021.Windows.ForAdministrator;
using SR57_2020_POP2021.Windows;
using System.Windows.Shapes;


namespace SR57_2020_POP2021.Windows.ForAdministrator
{
    public partial class ShowAttendeesWindow : Window
    {
        ICollectionView view;
        public ShowAttendeesWindow()
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;
        }

        private bool CustomFilter(object obj)
        {
            RegisteredUser user = obj as RegisteredUser;

            if (user.Role.Equals(ERole.Attendee) && user.Active)
            {
                if (txtSearchSurname.Text != "")
                {
                    return user.Surname.Contains(txtSearchSurname.Text);
                }
                if (txtSearchEmail.Text != "")
                {
                    return user.Email.Contains(txtSearchEmail.Text);
                }
                if (txtSearchName.Text != "")
                {
                    return user.Name.Contains(txtSearchName.Text);
                }
                else
                    return true;
            }
            return false;
        }

        private void UpdateView()
        {
            DGAttendees.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Users);
            DGAttendees.ItemsSource = view;
            DGAttendees.IsSynchronizedWithCurrentItem = true;

            DGAttendees.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGAttendees.SelectedItems.Clear();
        }

        private void AddAttendee_Click(object sender, RoutedEventArgs e)
        {
            RegisteredUser newUser = new RegisteredUser();
            AddEditAttendeesWindow addEditAttendees = new AddEditAttendeesWindow(newUser);
            this.Hide();
            if (!(bool)addEditAttendees.ShowDialog())
            {

            }
            this.Show();
            view.Refresh();
        }

        private void EditAttendee_Click(object sender, RoutedEventArgs e)
        {
            RegisteredUser selectedAttendee = view.CurrentItem as RegisteredUser;

            RegisteredUser oldAttendee = selectedAttendee.Clone();

            AddEditAttendeesWindow addEditAttendeesWindow = new AddEditAttendeesWindow(selectedAttendee, EStatus.Edit);
            this.Hide();
            if (!(bool)addEditAttendeesWindow.ShowDialog())
            {
                int index = Util.Instance.Users.ToList().FindIndex(user => user.Email.Equals(oldAttendee.Email));
                Util.Instance.Users[index] = oldAttendee;
            }
            this.Show();

            view.Refresh();
            DGAttendees.SelectedItems.Clear();
        }

        private void DeleteAttendee_Click(object sender, RoutedEventArgs e)
        {
            RegisteredUser attendeeForDeleting = view.CurrentItem as RegisteredUser;
            Util.Instance.DeleteUser(attendeeForDeleting.ID);

            int index = Util.Instance.Users.ToList().FindIndex(user => user.ID.Equals(attendeeForDeleting.ID));
            Util.Instance.Users[index].Active = false;

            UpdateView();
            view.Refresh();
        }

        private void DGAttendees_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Active"))
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void txtSearchSurname_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtSearchEmail_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtSearchName_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
