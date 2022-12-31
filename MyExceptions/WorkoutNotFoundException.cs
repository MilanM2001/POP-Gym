using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR57_2020_POP2021.MyExceptions
{
    public class WorkoutNotFoundException : Exception
    {
        public WorkoutNotFoundException() : base() { }
        public WorkoutNotFoundException(String message) : base(message) { }
        public WorkoutNotFoundException(String message, Exception innerException) : base(message, innerException) { }
    }
}
