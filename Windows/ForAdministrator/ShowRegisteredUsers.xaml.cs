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

namespace SR57_2020_POP2021.Windows.ForAdministrator
{

    public partial class ShowRegisteredUsers : Window
    {

        ICollectionView view;

        public ShowRegisteredUsers()
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;
        }

        private bool CustomFilter(object obj)
        {
            RegisteredUser user = obj as RegisteredUser;

            if (user.Active)
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
            DGRegisteredUsers.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Users);
            DGRegisteredUsers.ItemsSource = view;
            DGRegisteredUsers.IsSynchronizedWithCurrentItem = true;

            DGRegisteredUsers.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGRegisteredUsers.SelectedItems.Clear();
        }

        private void DGRegisteredUsers_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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

        private void txtSearchRole_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
