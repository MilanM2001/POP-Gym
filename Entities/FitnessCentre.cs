using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR57_2020_POP2021.Entities
{
    public class FitnessCentre
    {
        private int _id;
        private string fitnessCentreCode;
        private string centreName;
        private int centreAddress_ID;
        private bool active;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string FitnessCentreCode
        {
            get { return fitnessCentreCode; }
            set { fitnessCentreCode = value; }
        }

        public string CentreName
        {
            get { return centreName; }
            set { centreName = value; }
        }

        public int CentreAddress_ID
        {
            get { return centreAddress_ID; }
            set { centreAddress_ID = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public FitnessCentre() { }

        public FitnessCentre Clone()
        {
            FitnessCentre copy = new FitnessCentre();
            copy.ID = ID;
            copy.FitnessCentreCode = FitnessCentreCode;
            copy.CentreName = CentreName;
            copy.centreAddress_ID = centreAddress_ID;
            copy.Active = Active;

            return copy;
        }
    }
}