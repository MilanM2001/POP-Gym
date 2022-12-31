using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR57_2020_POP2021.MyExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base() { }
        public UserNotFoundException(String message) : base(message) { }
        public UserNotFoundException(String message, Exception innerException) : base(message, innerException) { }
    }
}
