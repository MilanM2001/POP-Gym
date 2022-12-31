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

namespace SR57_2020_POP2021.Windows.ForInstructor
{
    public partial class AddEditWorkoutsInstructor : Window
    {
        private EStatus selectedStatus;
        private Workout selectedWorkout;

        public AddEditWorkoutsInstructor(Workout workout, EStatus status = EStatus.Add)
        {
            InitializeComponent();

            this.DataContext = workout;
            ComboTypeWorkoutStatus.ItemsSource = Enum.GetValues(typeof(EWorkoutStatus)).Cast<EWorkoutStatus>();
            txtWorkoutDate.DisplayDateStart = DateTime.Today;


            using (SqlConnection sqlConnection = new SqlConnection(Util.CONNECTION_STRING))
            {
                SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Instructors", sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    ComboTypeAppointedInstructor.Items.Add(sqlReader["Id"]);
                }

                sqlReader.Close();
            }

            using (SqlConnection sqlConnection = new SqlConnection(Util.CONNECTION_STRING))
            {
                SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Attendees", sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    ComboTypeReservedForAttendee.Items.Add(sqlReader["Id"]);
                }

                sqlReader.Close();
            }

            selectedWorkout = workout;
            selectedStatus = status;

            if (status.Equals(EStatus.Edit) && workout != null)
            {
                this.Title = "Edit Workout";
                txtWorkoutCode.IsEnabled = false;
            }
            else
            {
                this.Title = "Add Workout";
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
                selectedWorkout.Active = true;
                Workout workout = new Workout();
                Util.Instance.Workouts.Add(workout);

                workout = selectedWorkout;

                Util.Instance.SaveEntity(workout);
            }
            else
            {
                selectedWorkout.Active = true;
                Workout workout = new Workout();

                workout = selectedWorkout;

                Util.Instance.UpdateEntity(workout);
            }

            this.DialogResult = true;
            this.Close();

        }

    }
}
