using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SR57_2020_POP2021.Entities;
using SR57_2020_POP2021.MyExceptions;
using System.Data.SqlClient;
using System.Data;

namespace SR57_2020_POP2021.Services
{
    class FitnessCentreService : IFitnessCentreService
    {
        public void DeleteFitnessCentre(int id)
        {
            FitnessCentre fitnessCentre = Util.Instance.FitnessCentres.ToList().Find(FitnessCentre => FitnessCentre.ID.Equals(id));
            if (fitnessCentre == null)
            {
                throw new WorkoutNotFoundException($"Fitness Centre with that ID doesn't exist: {id}");
            }

            fitnessCentre.Active = false;
            Console.WriteLine("Successfully deleted Fitness Centre with ID:" + id);
            UpdateFitnessCentre(fitnessCentre);
        }

        public void ReadFitnessCentre()
        {
            Util.Instance.FitnessCentres = new ObservableCollection<FitnessCentre>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedFitnessCentre = @"select * from FitnessCentre";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedFitnessCentre, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "FitnessCentre");
                foreach (DataRow row in ds.Tables["FitnessCentre"].Rows)
                {
                    Util.Instance.FitnessCentres.Add(new FitnessCentre
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        FitnessCentreCode = (string)row["FitnessCentreCode"],
                        CentreName = (string)row["CentreName"],
                        CentreAddress_ID = Convert.ToInt32(row["Address_ID"]),
                        Active = (bool)row["Active"]
                    });
                }
            }

        }

        public int SaveFitnessCentre(Object obj)
        {
            FitnessCentre fitnessCentre = obj as FitnessCentre;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.FitnessCentre (FitnessCentreCode, CentreName, Address_ID, Active)
                                        output inserted.id VALUES (@FitnessCentreCode, @CentreName, @Address_ID, @Active)";
                command.Parameters.Add(new SqlParameter("FitnessCentreCode", fitnessCentre.FitnessCentreCode));
                command.Parameters.Add(new SqlParameter("CentreName", fitnessCentre.CentreName));
                command.Parameters.Add(new SqlParameter("Address_ID", fitnessCentre.CentreAddress_ID));
                command.Parameters.Add(new SqlParameter("Active", fitnessCentre.Active));

                return (int)command.ExecuteScalar();
            }
        }

        public void UpdateFitnessCentre(object obj)
        {
            FitnessCentre fitnessCentre = obj as FitnessCentre;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                string selectedFitnessCentre = @"SELECT * from FitnessCentre";
                SqlDataAdapter adapter = new SqlDataAdapter(selectedFitnessCentre, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "FitnessCentre");

                foreach (DataRow row in ds.Tables["FitnessCentre"].Rows)
                {
                    if (row["FitnessCentreCode"].Equals(fitnessCentre.FitnessCentreCode))
                    {
                        row["FitnessCentreCode"] = fitnessCentre.FitnessCentreCode;
                        row["CentreName"] = fitnessCentre.CentreName;
                        row["Address_ID"] = fitnessCentre.CentreAddress_ID;
                        row["Active"] = fitnessCentre.Active;
                    }
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds.Tables["FitnessCentre"]);


            }
        }
    }
}
