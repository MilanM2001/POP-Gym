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

    public partial class ShowAdministratorsWindow : Window
    {
        ICollectionView view;
        public ShowAdministratorsWindow()
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;
        }

        private bool CustomFilter(object obj)
        {
            RegisteredUser user = obj as RegisteredUser;

            if (user.Role.Equals(ERole.Administrator) && user.Active)
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
            DGAdministrators.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Users);
            DGAdministrators.ItemsSource = view;
            DGAdministrators.IsSynchronizedWithCurrentItem = true;

            DGAdministrators.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGAdministrators.SelectedItems.Clear();
        }

        private void AddAdministrator_Click(object sender, RoutedEventArgs e)
        {
            RegisteredUser newUser = new RegisteredUser();
            AddEditAdministratorsWindow addEditAdministrators = new AddEditAdministratorsWindow(newUser);
            this.Hide();
            if (!(bool)addEditAdministrators.ShowDialog())
            {

            }
            this.Show();
            view.Refresh();
        }

        private void EditAdministrator_Click(object sender, RoutedEventArgs e)
        {
            RegisteredUser selectedAdministrator = view.CurrentItem as RegisteredUser;

            RegisteredUser oldAdministrator = selectedAdministrator.Clone();

            AddEditAdministratorsWindow addEditAdministratorsWindow = new AddEditAdministratorsWindow(selectedAdministrator, EStatus.Edit);
            this.Hide();
            if (!(bool)addEditAdministratorsWindow.ShowDialog())
            {
                int index = Util.Instance.Users.ToList().FindIndex(user => user.Email.Equals(oldAdministrator.Email));
                Util.Instance.Users[index] = oldAdministrator;
            }
            this.Show();

            view.Refresh();
            DGAdministrators.SelectedItems.Clear();
        }

        private void DeleteAdministrator_Click(object sender, RoutedEventArgs e)
        {
            RegisteredUser administratorForDeleting = view.CurrentItem as RegisteredUser;
            Util.Instance.DeleteUser(administratorForDeleting.ID);

            int index = Util.Instance.Users.ToList().FindIndex(user => user.ID.Equals(administratorForDeleting.ID));
            Util.Instance.Users[index].Active = false;


            UpdateView();
            view.Refresh();
        }

        private void DGAdministrators_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
