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

    public partial class ShowAddressesWindow : Window
    {
        ICollectionView view;
        public ShowAddressesWindow()
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;
        }

        private bool CustomFilter(object obj)
        {
            Address address = obj as Address;

            if (address.Active)
            {
                if (txtSearch.Text != "")
                {
                    return address.City.Contains(txtSearch.Text);
                }
                else
                    return true;
            }
            return false;
        }

        private void UpdateView()
        {
            DGAddresses.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Addresses);
            DGAddresses.ItemsSource = view;
            DGAddresses.IsSynchronizedWithCurrentItem = true;

            DGAddresses.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGAddresses.SelectedItems.Clear();
        }

        private void AddAddress_Click(object sender, RoutedEventArgs e)
        {
            Address newAddress = new Address();
            AddEditAddressesWindow addEditAddresses = new AddEditAddressesWindow(newAddress);
            Hide();
            if (!(bool)addEditAddresses.ShowDialog())
            {

            }
            UpdateView();
            Show();
            view.Refresh();
        }

        private void EditAddress_Click(object sender, RoutedEventArgs e)
        {
            Address selectedAddress = view.CurrentItem as Address;

            Address oldAddress = selectedAddress.Clone();

            AddEditAddressesWindow addEditAddresses = new AddEditAddressesWindow(selectedAddress, EStatus.Edit);
            this.Hide();
            if (!(bool)addEditAddresses.ShowDialog())
            {
                int index = Util.Instance.Addresses.ToList().FindIndex(address => address.ID.Equals(oldAddress.ID));
                Util.Instance.Addresses[index] = oldAddress;
            }
            this.Show();

            DGAddresses.SelectedItems.Clear();
        }

        private void DeleteAddress_Click(object sender, RoutedEventArgs e)
        {
            Address addressForDeleting = view.CurrentItem as Address;
            Util.Instance.DeleteAddress(addressForDeleting.ID);

            int index = Util.Instance.Addresses.ToList().FindIndex(address => address.ID.Equals(addressForDeleting.ID));
            Util.Instance.Addresses[index].Active = false;


            UpdateView();
            view.Refresh();
        }

        private void DGAddresses_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Active"))
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void txtSearch_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
