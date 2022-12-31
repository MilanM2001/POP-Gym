using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SR57_2020_POP2021.Entities;
using SR57_2020_POP2021.MyExceptions;
using System.Data.SqlClient;
using System.Data;

namespace SR57_2020_POP2021.Services
{
    public class WorkoutService : IWorkoutService
    {
        public void DeleteWorkouts(int id)
        {
            Workout workout = Util.Instance.Workouts.ToList().Find(Workout => Workout.ID.Equals(id));
            if (workout == null)
            {
                throw new WorkoutNotFoundException($"Workout with that ID doesn't exist: {id}");
            }

            workout.Active = false;
            Console.WriteLine("Successfully deleted Workout with ID:" + id);
            UpdateWorkout(workout);
        }

        public void ReadWorkouts()
        {
            Util.Instance.Workouts = new ObservableCollection<Workout>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedWorkout = @"select * from Workouts";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedWorkout, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Workouts");
                foreach (DataRow row in ds.Tables["Workouts"].Rows)
                {
                    Util.Instance.Workouts.Add(new Workout
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        WorkoutCode = (string)row["WorkoutCode"],
                        WorkoutDate = (DateTime)row["WorkoutDate"],
                        WorkoutStartTime = (string)row["WorkoutStartTime"],
                        WorkoutLength = (string)row["WorkoutLength"],
                        WorkoutStatus = (EWorkoutStatus)Enum.Parse(typeof(EWorkoutStatus), row["WorkoutStatus"].ToString(), true),
                        AppointedInstructor_ID = Convert.ToInt32(row["AppointedInstructor_ID"]),
                        ReservedForAttendee_ID = Convert.ToInt32(row["ReservedForAttendee_ID"]),
                        Active = (bool)row["Active"]
                    });
                }
            }
        }

        public int SaveWorkouts(Object obj)
        {
            Workout workout = obj as Workout;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Workouts (WorkoutCode, WorkoutDate, WorkoutStartTime, WorkoutLength, WorkoutStatus, ReservedForAttendee_ID, AppointedInstructor_ID, Active)
                                        output inserted.id Values (@WorkoutCode, @WorkoutDate, @WorkoutStartTime, @WorkoutLength, @WorkoutStatus, @ReservedForAttendee_ID, @AppointedInstructor_ID, @Active)";
                command.Parameters.Add(new SqlParameter("WorkoutCode", workout.WorkoutCode));
                command.Parameters.Add(new SqlParameter("WorkoutDate", workout.WorkoutDate.ToString()));
                command.Parameters.Add(new SqlParameter("WorkoutStartTime", workout.WorkoutStartTime));
                command.Parameters.Add(new SqlParameter("WorkoutLength", workout.WorkoutLength));
                command.Parameters.Add(new SqlParameter("WorkoutStatus", workout.WorkoutStatus.ToString()));
                command.Parameters.Add(new SqlParameter("AppointedInstructor_ID", workout.AppointedInstructor_ID));
                command.Parameters.Add(new SqlParameter("ReservedForAttendee_ID", workout.ReservedForAttendee_ID));
                command.Parameters.Add(new SqlParameter("Active", workout.Active));

                return (int)command.ExecuteScalar();
            }
        }

        public void UpdateWorkout(object obj)
        {
            Workout workout = obj as Workout;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedWorkout = @"SELECT * from Workouts";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedWorkout, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "Workouts");

                foreach (DataRow row in ds.Tables["Workouts"].Rows)
                {
                    if (row["WorkoutCode"].Equals(workout.WorkoutCode))
                    {
                        row["WorkoutCode"] = workout.WorkoutCode;
                        row["WorkoutDate"] = workout.WorkoutDate.ToString();
                        row["WorkoutStartTime"] = workout.WorkoutStartTime;
                        row["WorkoutLength"] = workout.WorkoutLength;
                        row["WorkoutStatus"] = workout.WorkoutStatus.ToString();
                        row["AppointedInstructor_ID"] = workout.AppointedInstructor_ID;
                        row["ReservedForAttendee_ID"] = workout.ReservedForAttendee_ID;
                        row["Active"] = workout.Active;
                    }
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds.Tables["Workouts"]);


            }
        }
    }

}
