using SR57_2020_POP2021.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SR57_2020_POP2021.Windows.ForUnregistered
{

    public partial class UnregisteredViewFitnessCentreWindow : Window
    {

        ICollectionView view;

        public UnregisteredViewFitnessCentreWindow()
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;
        }

        private bool CustomFilter(object obj)
        {
            FitnessCentre fitnessCentre = obj as FitnessCentre;

            if (fitnessCentre.Active)
            {
                if (txtSearch.Text != "")
                {
                    return fitnessCentre.CentreName.Contains(txtSearch.Text);
                }
                else
                    return true;
            }
            return false;
        }

        private void UpdateView()
        {
            DGFitnessCentre.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.FitnessCentres);
            DGFitnessCentre.ItemsSource = view;
            DGFitnessCentre.IsSynchronizedWithCurrentItem = true;

            DGFitnessCentre.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGFitnessCentre.SelectedItems.Clear();
        }

        private void DGFitnessCentre_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Active"))
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void txtSearch_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
