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

    public partial class AdministratorShowInfo : Window
    {

        ICollectionView view;

        public AdministratorShowInfo(RegisteredUser registeredUser)
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;

            bool CustomFilter(object obj)
            {
                RegisteredUser user = obj as RegisteredUser;

                if (user.Role.Equals(ERole.Administrator) && user.Active && user.Email.Equals(registeredUser.Email))
                {
                    return true;
                }
                return false;
            }
        }

        private void UpdateView()
        {
            DGAdministrators.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Users);
            DGAdministrators.ItemsSource = view;
            DGAdministrators.IsSynchronizedWithCurrentItem = true;

            DGAdministrators.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGAdministrators_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Active"))
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void EditAdministrator_Click(object sender, RoutedEventArgs e)
        {
            RegisteredUser selectedAdministrator = view.CurrentItem as RegisteredUser;

            RegisteredUser oldAdministrator = selectedAdministrator.Clone();

            AdministratorEditInfo administratorEditInfo = new AdministratorEditInfo(selectedAdministrator, EStatus.Edit);
            this.Hide();
            if (!(bool)administratorEditInfo.ShowDialog())
            {
                int index = Util.Instance.Users.ToList().FindIndex(user => user.Email.Equals(oldAdministrator.Email));
                Util.Instance.Users[index] = oldAdministrator;
            }
            this.Show();

            view.Refresh();
            DGAdministrators.SelectedItems.Clear();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }

    }
}
