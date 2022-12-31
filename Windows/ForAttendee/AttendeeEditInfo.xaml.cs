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

namespace SR57_2020_POP2021.Windows.ForAttendee
{
    public partial class AttendeeEditInfo : Window
    {

        private EStatus selectedStatus;
        private RegisteredUser selectedAttendee;

        public AttendeeEditInfo(RegisteredUser attendee, EStatus status = EStatus.Add)
        {
            InitializeComponent();

            this.DataContext = attendee;
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


            selectedAttendee = attendee;
            selectedStatus = status;

            if (status.Equals(EStatus.Edit) && attendee != null)
            {
                this.Title = "Edit Attendee";
                txtJMBG.IsEnabled = false;
                ComboTypeRole.IsEnabled = false;
            }
            else
            {
                this.Title = "Add Attendee";
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
                    selectedAttendee.Active = true;
                    Attendee attendee = new Attendee
                    {
                        User = selectedAttendee
                    };
                    Util.Instance.Users.Add(selectedAttendee);
                    Util.Instance.Attendees.Add(attendee);

                    int id = Util.Instance.SaveEntity(selectedAttendee);
                    attendee.ID = id;
                    Util.Instance.SaveEntity(attendee);
                }
                else
                {
                    selectedAttendee.Active = true;
                    Attendee attendee = new Attendee
                    {
                        User = selectedAttendee
                    };

                    Util.Instance.UpdateEntity(selectedAttendee);
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
