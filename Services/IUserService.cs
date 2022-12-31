using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR57_2020_POP2021.Services
{
    public interface IUserService
    {
        int SaveUser(Object obj);
        void ReadUsers();
        void DeleteUser(int id);
        void UpdateUser(Object obj);
    }
}
