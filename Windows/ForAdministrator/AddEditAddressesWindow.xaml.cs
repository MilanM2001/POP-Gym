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
using SR57_2020_POP2021.Entities;
using System.Windows.Shapes;

namespace SR57_2020_POP2021.Windows.ForAdministrator
{
    public partial class AddEditAddressesWindow : Window
    {
        private EStatus selectedStatus;
        private Address selectedAddress;

        public AddEditAddressesWindow(Address address, EStatus status = EStatus.Add)
        {
            InitializeComponent();

            this.DataContext = address;

            selectedAddress = address;
            selectedStatus = status;

            if (status.Equals(EStatus.Edit) && address != null)
            {
                this.Title = "Edit Address";
                txtAddressCode.IsEnabled = false;
            }
            else
            {
                this.Title = "Add Address";
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            if (selectedStatus.Equals(EStatus.Add))
            {
                selectedAddress.Active = true;
                Address address = new Address();
                Util.Instance.Addresses.Add(address);

                address = selectedAddress;

                Util.Instance.SaveEntity(address);
            }
            else 
            {
                selectedAddress.Active = true;
                Address address = new Address();

                address = selectedAddress;

                Util.Instance.UpdateEntity(address);
            }

            this.DialogResult = true;
            this.Close();

        }
        
    }
}
