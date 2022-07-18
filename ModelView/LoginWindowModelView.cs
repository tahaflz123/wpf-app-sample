using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;
using WpfApp1.View;
using WpfApp1.Model;

namespace WpfApp1.ModelView
{
    class LoginWindowModelView : INotifyPropertyChanged
    {

        private LoginWindow loginWindow { get; set; }
        private UserService userService { get; set; }

        public bool loginSucces { get; set; }


        public readonly DelegateCommand _loginClick;

        public ICommand loginClick => _loginClick;


        private string _name { get; set; }

        public string name { get => _name; set { _name = value; OnPropertyChange("name"); } }

        private string _password { get; set; }

        public string password { get => _password; 
                set {
                _password = value;
                OnPropertyChange("password");
            } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public LoginWindowModelView() { }

        public LoginWindowModelView(LoginWindow loginWindow)
        {
            userService = new UserService();
            this.loginWindow = loginWindow;
            loginSucces = false;
            _loginClick = new DelegateCommand(login);
        }

        public void login(object e)
        {
            User user = userService.findUserByName(name);

            if (!user.name.Equals(name))
            {
                return;
            }

            var passwordBox = e as PasswordBox;
            var password = passwordBox.Password;

            if (!password.Equals(user.password))
            {
                return;
            }


            MainWindow mainWindow = new MainWindow();

            loginWindow.Close();
            mainWindow.Show();

        }





    }
}
