using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR57_2020_POP2021.Entities
{
    [Serializable]

    public class Instructor
    {
        private int _id;
        private RegisteredUser _user;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }


        public RegisteredUser User
        {
            get { return _user; }
            set { _user = value; }
        }

    }
}
