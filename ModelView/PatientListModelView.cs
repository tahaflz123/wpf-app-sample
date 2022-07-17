using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using WpfApp1.Model;
using System.Windows.Controls;
using System.Windows;
using WpfApp1.View;

namespace WpfApp1.ModelView
{



    public class PatientListModelView : INotifyPropertyChanged
    {

        PatientList patientListPage { get; set; }

        PatientService patientService = null;

        public List<Patient> Patients { get; set; }

        ListView listView { get; set; }

        MainWindow mainWindow { get; set; }

        private string _searchText;
        public string searchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChange("searchText");
                
            }

        }

        public readonly DelegateCommand _refreshUserList, _deleteUser, _createUser, _searchBox, _mainPageClick;


        public ICommand refreshUserList => _refreshUserList;

        public ICommand createUser => _createUser;

        public ICommand deleteUser => _deleteUser;

        public ICommand searchBox => _searchBox;

        public ICommand mainPageClick => _mainPageClick;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                if (_searchText == null || _searchText.Length == 0 || _searchText == "")
                {
                    this.patientListPage.patientListView.ItemsSource = Patients;

                }

                this.patientListPage.patientListView.ItemsSource = Patients.FindAll(user => user.name.Contains(this.searchText) || user.surname.Contains(this.searchText));
            }
        }


        public PatientListModelView() { }
        public PatientListModelView(PatientList patientListPage)
        {
            this.patientListPage = patientListPage;
            _refreshUserList = new DelegateCommand(refreshUserList_Click);
            _createUser = new DelegateCommand(createUser_Click);
            _deleteUser = new DelegateCommand(deleteUser_Click);
            _mainPageClick = new DelegateCommand(mainPage_Click);
            listView = patientListPage.patientListView;
            patientService = new PatientService();
            Patients = patientService.findAll();
            listView.ItemsSource = Patients;
        }
        
        public void refreshUserList_Click(object inputParameters)
        {
            Patients = patientService.refreshPatients();
            listView.ItemsSource = Patients;
        }
        public void deleteUser_Click(object inputParameters)
        {

            Patient patient = getSelectedPatient();
            if(patient == null)
            {
                return;
            }

            MessageBoxResult result = MessageBox.Show("User will be delete,\n" + patient.ToString(), "User deleting", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Cancel);

            if (result == MessageBoxResult.No)
            {
                return;
            }
            patientService.deletePatient(getSelectedPatient());
            Patients = patientService.refreshPatients();
            listView.ItemsSource = Patients;

             
            MessageBox.Show("User successfully deleted,\n" + patient.ToString(), "User deleted", MessageBoxButton.OK, MessageBoxImage.Information);


        }
        public void createUser_Click(object inputParameters)
        {
            var mainWindow = patientListPage.Parent as MainWindow;
            mainWindow.Content = new PatientCreatePage();
        }

        public void mainPage_Click(object e)
        {
            MainPage mainPage = new MainPage();
            var mainWindow = patientListPage.Parent as MainWindow;
            mainWindow.Content = mainPage;
            mainWindow.DataContext = mainPage;
        }


        public Patient getSelectedPatient()
        {
            Patient patient = this.listView.SelectedItem as Patient;
            return patient;
        }
    }

}