using SR57_2020_POP2021.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

    public partial class AdministratorEditInfo : Window
    {

        private EStatus selectedStatus;
        private RegisteredUser selectedAdministrator;

        public AdministratorEditInfo(RegisteredUser administrator, EStatus status = EStatus.Add)
        {
            InitializeComponent();

            this.DataContext = administrator;
            ComboTypeRole.ItemsSource = Enum.GetValues(typeof(ERole)).Cast<ERole>();
            ComboTypeGender.ItemsSource = Enum.GetValues(typeof(EGender)).Cast<EGender>();

            using (SqlConnection sqlConnection = new SqlConnection(Util.CONNECTION_STRING))
            {
                SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Address WHERE Active = 1", sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    ComboTypeAddress.Items.Add(sqlReader["Id"]);
                }

                sqlReader.Close();
            }


            selectedAdministrator = administrator;
            selectedStatus = status;

            if (status.Equals(EStatus.Edit) && administrator != null)
            {
                this.Title = "Edit Administrator";
                txtJMBG.IsEnabled = false;
                ComboTypeRole.IsEnabled = false;
            }
            else
            {
                this.Title = "Add Administrator";
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {

                if (selectedStatus.Equals(EStatus.Add))
                {
                    selectedAdministrator.Active = true;
                    Administrator administrator = new Administrator
                    {
                        User = selectedAdministrator
                    };
                    Util.Instance.Users.Add(selectedAdministrator);
                    Util.Instance.Administrators.Add(administrator);

                    int id = Util.Instance.SaveEntity(selectedAdministrator);
                    administrator.ID = id;
                    Util.Instance.SaveEntity(administrator);
                }
                else
                {
                    selectedAdministrator.Active = true;
                    Administrator administrator = new Administrator
                    {
                        User = selectedAdministrator
                    };

                    Util.Instance.UpdateEntity(selectedAdministrator);
                }

                this.DialogResult = true;
                this.Close();
            }
        }
        private bool IsValid()
        {
            return !Validation.GetHasError(txtJMBG) && !Validation.GetHasError(txtEmail) && !Validation.GetHasError(txtName);
        }

    }
}
