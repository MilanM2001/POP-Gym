using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR57_2020_POP2021.Entities
{
    public class Workout
    {
        private int _id;
        private string workoutCode;
        private DateTime workoutDate;
        private string workoutStartTime;
        private string workoutLength;
        private EWorkoutStatus workoutStatus;
        private int appointedInstructor_ID;
        private int reservedForAttendee_ID;
        private bool active;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string WorkoutCode
        {
            get { return workoutCode; }
            set { workoutCode = value; }
        }

        public DateTime WorkoutDate
        {
            get { return workoutDate; }
            set { workoutDate = value; }
        }

        public string WorkoutStartTime
        {
            get { return workoutStartTime; }
            set { workoutStartTime = value; }
        }

        public string WorkoutLength
        {
            get { return workoutLength; }
            set { workoutLength = value; }
        }

        public EWorkoutStatus WorkoutStatus
        {
            get { return workoutStatus; }
            set { workoutStatus = value; }
        }

        public int AppointedInstructor_ID
        {
            get { return appointedInstructor_ID; }
            set { appointedInstructor_ID = value; }
        }

        public int ReservedForAttendee_ID
        {
            get { return reservedForAttendee_ID; }
            set { reservedForAttendee_ID = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public Workout() { }

        public Workout(string workoutCode, string workoutStartTime)
        {
            WorkoutCode = workoutCode;
            WorkoutStartTime = workoutStartTime;
        }

        public Workout Clone()
        {
            Workout copy = new Workout();
            copy.ID = ID;
            copy.WorkoutCode = WorkoutCode;
            copy.WorkoutDate = WorkoutDate;
            copy.WorkoutStartTime = WorkoutStartTime;
            copy.WorkoutLength = WorkoutLength;
            copy.WorkoutStatus = WorkoutStatus;
            copy.AppointedInstructor_ID = AppointedInstructor_ID;
            copy.ReservedForAttendee_ID = ReservedForAttendee_ID;
            copy.Active = Active;

            return copy;
        }
    }
}
