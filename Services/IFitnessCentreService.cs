using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR57_2020_POP2021.Services
{
    public interface IFitnessCentreService
    {
        int SaveFitnessCentre(Object obj);
        void ReadFitnessCentre();
        void DeleteFitnessCentre(int id);
        void UpdateFitnessCentre(Object obj);
    }
}
