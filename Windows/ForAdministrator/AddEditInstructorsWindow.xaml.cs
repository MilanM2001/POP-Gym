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
using System.Data.SqlClient;
using System.Data;

namespace SR57_2020_POP2021.Windows.ForAdministrator
{

    public partial class AddEditInstructorsWindow : Window
    {
        private EStatus selectedStatus;
        private RegisteredUser selectedInstructor;

        public AddEditInstructorsWindow(RegisteredUser instructor, EStatus status = EStatus.Add)
        {
            InitializeComponent();

            this.DataContext = instructor;
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


            selectedInstructor = instructor;
            selectedStatus = status;

            if (status.Equals(EStatus.Edit) && instructor != null)
            {
                this.Title = "Edit Instructor";
                txtJMBG.IsEnabled = false;
                ComboTypeRole.IsEnabled = false;
            }
            else
            {
                this.Title = "Add Instructor";
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
                    selectedInstructor.Active = true;
                    Instructor instructor = new Instructor
                    {
                        User = selectedInstructor
                    };
                    Util.Instance.Users.Add(selectedInstructor);
                    Util.Instance.Instructors.Add(instructor);

                    int id = Util.Instance.SaveEntity(selectedInstructor);
                    instructor.ID = id;
                    Util.Instance.SaveEntity(instructor);
                }
                else
                {
                    selectedInstructor.Active = true;
                    Instructor instructor = new Instructor
                    {
                        User = selectedInstructor
                    };

                    Util.Instance.UpdateEntity(selectedInstructor);
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
