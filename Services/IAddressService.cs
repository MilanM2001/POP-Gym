using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR57_2020_POP2021.Services
{
    public interface IAddressService
    {
        int SaveAddress(Object obj);
        void ReadAddress();
        void DeleteAddress(int id);
        void UpdateAddress(Object obj);
    }
}
