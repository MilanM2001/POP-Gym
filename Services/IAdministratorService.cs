using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR57_2020_POP2021.Entities;

namespace SR57_2020_POP2021.Services
{
    public interface IAdministratorService
    {
        int SaveUser(Object obj);
        void ReadUsers();
        void UpdateAdministrator(Object obj);
        List<RegisteredUser> FindAllClients(String email);
    }
}
