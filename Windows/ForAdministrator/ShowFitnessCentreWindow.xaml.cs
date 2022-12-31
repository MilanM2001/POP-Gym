using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using SR57_2020_POP2021.Entities;
using SR57_2020_POP2021.Windows;
using System.Windows.Shapes;

namespace SR57_2020_POP2021.Windows.ForAdministrator
{
    public partial class ShowFitnessCentreWindow : Window
    {
        ICollectionView view;
        public ShowFitnessCentreWindow()
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

        private void AddFitnessCentre_Click(object sender, RoutedEventArgs e)
        {
            FitnessCentre newFitnessCentre = new FitnessCentre();
            AddEditFitnessCentreWindow addEditFitnessCentre = new AddEditFitnessCentreWindow(newFitnessCentre);
            this.Hide();
            if (!(bool)addEditFitnessCentre.ShowDialog())
            {

            }
            this.Show();
            view.Refresh();
        }

        private void EditFitnessCentre_Click(object sender, RoutedEventArgs e)
        {
            FitnessCentre selectedFitnessCentre = view.CurrentItem as FitnessCentre;

            FitnessCentre oldFitnessCentre = selectedFitnessCentre.Clone();

            AddEditFitnessCentreWindow addEditFitnessCentre = new AddEditFitnessCentreWindow(selectedFitnessCentre, EStatus.Edit);
            this.Hide();
            if (!(bool)addEditFitnessCentre.ShowDialog())
            {
                int index = Util.Instance.FitnessCentres.ToList().FindIndex(FitnessCentre => FitnessCentre.ID.Equals(oldFitnessCentre.ID));
                Util.Instance.FitnessCentres[index] = oldFitnessCentre;
            }
            this.Show();

            view.Refresh();
            DGFitnessCentre.SelectedItems.Clear();
        }

        private void DeleteFitnessCentre_Click(object sender, RoutedEventArgs e)
        {
            FitnessCentre fitnessCentreForDeleting = view.CurrentItem as FitnessCentre;
            Util.Instance.DeleteFitnessCentre(fitnessCentreForDeleting.ID);

            int index = Util.Instance.FitnessCentres.ToList().FindIndex(FitnessCentre => FitnessCentre.ID.Equals(fitnessCentreForDeleting.ID));
            Util.Instance.FitnessCentres[index].Active = false;

            UpdateView();
            view.Refresh();
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
