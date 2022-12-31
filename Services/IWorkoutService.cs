using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR57_2020_POP2021.Services
{
    public interface IWorkoutService
    {
        int SaveWorkouts(Object obj);
        void ReadWorkouts();
        void DeleteWorkouts(int id);
        void UpdateWorkout(Object ojb);
    }
}
