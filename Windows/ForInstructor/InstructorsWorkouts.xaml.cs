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
using System.ComponentModel;

namespace SR57_2020_POP2021.Windows.ForInstructor
{
    public partial class InstructorsWorkouts : Window
    {

        ICollectionView view;

        public InstructorsWorkouts(RegisteredUser registeredUser)
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;
            Title = registeredUser.Name + " Workouts" + " ID=" + registeredUser.ID;

            bool CustomFilter(object obj)
            {
                Workout workout = obj as Workout;

                if (workout.Active && workout.AppointedInstructor_ID.Equals(registeredUser.ID))
                {
                    if (txtSearch.Text != "")
                    {
                        return workout.WorkoutStartTime.Contains(txtSearch.Text);
                    }
                    else
                        return true;
                }
                return false;
            }

        }
        

        private void UpdateView()
        {
            DGWorkouts.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Workouts);
            DGWorkouts.ItemsSource = view;
            DGWorkouts.IsSynchronizedWithCurrentItem = true;

            DGWorkouts.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGWorkouts.SelectedItems.Clear();
        }

        private void AddWorkout_Click(object sender, RoutedEventArgs e)
        {
            Workout newWorkout = new Workout();
            AddEditWorkoutsInstructor addEditWorkoutsInstructor = new AddEditWorkoutsInstructor(newWorkout);
            this.Hide();
            if (!(bool)addEditWorkoutsInstructor.ShowDialog())
            {

            }
            view.Refresh();
            this.Show();
        }

        private void EditWorkout_Click(object sender, RoutedEventArgs e)
        {
            Workout selectedWorkout = view.CurrentItem as Workout;

            Workout oldWorkout = selectedWorkout.Clone();

            AddEditWorkoutsInstructor addEditWorkouts = new AddEditWorkoutsInstructor(selectedWorkout, EStatus.Edit);
            this.Hide();
            if (!(bool)addEditWorkouts.ShowDialog())
            {
                int index = Util.Instance.Workouts.ToList().FindIndex(workout => workout.ID.Equals(oldWorkout.ID));
                Util.Instance.Workouts[index] = oldWorkout;
            }
            this.Show();

            view.Refresh();
            DGWorkouts.SelectedItems.Clear();
        }

        private void DeleteWorkout_Click(object sender, RoutedEventArgs e)
        {
            Workout workoutForDeleting = view.CurrentItem as Workout;
            Util.Instance.DeleteWorkout(workoutForDeleting.ID);

            int index = Util.Instance.Workouts.ToList().FindIndex(workout => workout.ID.Equals(workoutForDeleting.ID));
            Util.Instance.Workouts[index].Active = false;


            UpdateView();
            view.Refresh();
        }

        private void DGWorkouts_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
