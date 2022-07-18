using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

using WpfApp1.Model;
using WpfApp1.View;

namespace WpfApp1.ModelView
{
    public class PatientCreatePageModelView : INotifyPropertyChanged
    {

        private int _age;

        public int Age
        {
            get => _age;
            set
            {
                if (value < 0 || value > 200)
                {
                    return;
                }
                else
                {
                    _age = value;
                    OnPropertyChange("Age");
                }
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChange("Name");
            }

        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChange("Surname");
            }
        }
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChange("Email");
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChange("Password");
            }
        }

        private object _genderValue;

        public object GenderValue
        {
            get { return _genderValue; }
            set
            {
                var comboBoxValue = value as ComboBoxItem;

                Gender = comboBoxValue.Content.ToString();
                OnPropertyChange("GenderValue");


            }
        }

        private string _gender;

        public string Gender
        {
            get => _gender;

            set
            {
                _gender = value;
            }

        }


        PatientService patientService = null;


        public PatientCreatePage patientCreatePage { get; set; }



        public readonly DelegateCommand _savePatientButtonClick, _navigateToUserList, _ageUpCommand, _ageDownCommand, _genderSelectedCommand, _mainPageClick;

        public ICommand savePatientButtonClick => _savePatientButtonClick;


        public ICommand navigateToUserList => _navigateToUserList;

        public ICommand cmdAgeUp => _ageUpCommand;
        public ICommand cmdAgeDown => _ageDownCommand;

        public ICommand genderSelectedCommand => _genderSelectedCommand;

        public ICommand mainPageClick => _mainPageClick;


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        public PatientCreatePageModelView(PatientCreatePage patientCreatePage)
        {

            this.patientCreatePage = patientCreatePage;
            patientService = new PatientService();
            _saveUserCommand = new DelegateCommand(savePatientButton_Click);
            _navigateToUserList = new DelegateCommand(navigateToUserList_Click);
            _ageUpCommand = new DelegateCommand(ageUpCommand_Click);
            _ageDownCommand = new DelegateCommand(ageDownCommand_Click);
            _mainPageClick = new DelegateCommand(mainPage_Click);
            //_genderSelectedCommand = new DelegateCommand(genderSelectedCommand_Selected);

        }




        private void savePatientButton_Click(object commandParameter)
        {
            this.patientService.savePatient(new Patient(Name, Surname, Email,Password, Age, Gender, null));

            

            MessageBox.Show("Patient succesfully saved!", "Patient saved", MessageBoxButton.OK);

        }

        private void navigateToUserList_Click(object commandParameter)
        {
            //patientCreatePage.NavigationService.Navigate(new PatientList());
            PatientList patientList = new PatientList();
            var mainWindow = patientCreatePage.Parent as MainWindow;
            mainWindow.Content = patientList;
        }

        private void ageUpCommand_Click(object parameters)
        {
            Age++;
        }
        
        private void ageDownCommand_Click(object parameters)
        {
            Age--;
        }


        private void mainPage_Click(object e)
        {
            MainPage mainPage = new MainPage();
            var mainWindow = this.patientCreatePage.Parent as MainWindow;
            mainWindow.Content = mainPage;
            mainWindow.DataContext = mainPage;
        }




    }
}
