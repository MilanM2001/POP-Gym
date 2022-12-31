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
using SR57_2020_POP2021.Windows;
using System.Windows.Shapes;

namespace SR57_2020_POP2021.Windows.ForAdministrator
{
    public partial class ShowInstructorsWindow : Window
    {
        ICollectionView view;
        public ShowInstructorsWindow()
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;
        }

        private bool CustomFilter(object obj)
        {
            RegisteredUser user = obj as RegisteredUser;

            if (user.Role.Equals(ERole.Instructor) && user.Active)
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
            DGInstructors.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Users);
            DGInstructors.ItemsSource = view;
            DGInstructors.IsSynchronizedWithCurrentItem = true;

            DGInstructors.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGInstructors.SelectedItems.Clear();
        }

        private void AddInstructor_Click(object sender, RoutedEventArgs e)
        {
            RegisteredUser newUser = new RegisteredUser();
            AddEditInstructorsWindow addEditInstructors = new AddEditInstructorsWindow(newUser);
            this.Hide();
            if (!(bool)addEditInstructors.ShowDialog())
            {

            }
            this.Show();

            view.Refresh();
            DGInstructors.SelectedItems.Clear();
        }

        private void EditInstructor_Click(object sender, RoutedEventArgs e)
        {
            RegisteredUser selectedInstructor = view.CurrentItem as RegisteredUser;

            RegisteredUser oldInstructor = selectedInstructor.Clone();

            AddEditInstructorsWindow addEditInstructors = new AddEditInstructorsWindow(selectedInstructor, EStatus.Edit);
            this.Hide();
            if (!(bool)addEditInstructors.ShowDialog())
            {
                int index = Util.Instance.Users.ToList().FindIndex(user => user.Email.Equals(oldInstructor.Email));
                Util.Instance.Users[index] = oldInstructor;
            }
            this.Show();

            view.Refresh();
            DGInstructors.SelectedItems.Clear();
        }

        private void DeleteInstructor_Click(object sender, RoutedEventArgs e)
        {
            RegisteredUser instructorForDeleting = view.CurrentItem as RegisteredUser;
            Util.Instance.DeleteUser(instructorForDeleting.ID);

            int index = Util.Instance.Users.ToList().FindIndex(user => user.ID.Equals(instructorForDeleting.ID));
            Util.Instance.Users[index].Active = false;


            UpdateView();
            view.Refresh();
        }

        private void DGInstructors_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
