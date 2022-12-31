using SR57_2020_POP2021.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SR57_2020_POP2021.Windows.ForAdministrator
{
    public partial class AddEditFitnessCentreWindow : Window
    {
        private EStatus selectedStatus;
        private FitnessCentre selectedFitnessCentre;

        public AddEditFitnessCentreWindow(FitnessCentre fitnessCentre, EStatus status = EStatus.Add)
        {
            InitializeComponent();

            this.DataContext = fitnessCentre;

            using (SqlConnection sqlConnection = new SqlConnection(Util.CONNECTION_STRING))
            {
                SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Address", sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    ComboTypeAddress.Items.Add(sqlReader["Id"]);
                }

                sqlReader.Close();
            }

            selectedFitnessCentre = fitnessCentre;
            selectedStatus = status;

            if (status.Equals(EStatus.Edit) && fitnessCentre != null)
            {
                this.Title = "Edit Fitness Centre";
                txtFitnessCentreCode.IsEnabled = false;
            }
            else
            {
                this.Title = "Add Fitness Centre";
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            if (selectedStatus.Equals(EStatus.Add))
            {
                selectedFitnessCentre.Active = true;
                FitnessCentre fitnessCentre = new FitnessCentre();
                Util.Instance.FitnessCentres.Add(fitnessCentre);

                fitnessCentre = selectedFitnessCentre;

                Util.Instance.SaveEntity(fitnessCentre);
            }
            else
            {
                selectedFitnessCentre.Active = true;
                FitnessCentre fitnessCentre = new FitnessCentre();

                fitnessCentre = selectedFitnessCentre;

                Util.Instance.UpdateEntity(fitnessCentre);
            }

            this.DialogResult = true;
            this.Close();

        }

    }
}
