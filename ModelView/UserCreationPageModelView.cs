using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using WpfApp1.View;
using WpfApp1.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace WpfApp1.ModelView
{
    class UserCreationPageModelView : INotifyPropertyChanged
    {


        private UserService userService { get; set; }

        private UserCreationPage _userCreationPage { get; set; }


        private string _name { get; set; }

        public string name { get => _name; set { _name = value; PropertyChange("name"); } }

        private string _surname { get; set; }

        public string surname { get => _surname; set { _surname = value; PropertyChange("surname"); } }

        private string _phoneNumber { get; set; }

        public string phoneNumber { get => _phoneNumber; set { _phoneNumber = value; PropertyChange("phoneNumber"); } }

        private string _email { get; set; }

        public string email { get => _email; set { _email = value; PropertyChange("email"); } }

        private string _password { get; set; }


        public readonly DelegateCommand _saveClick;

        public ICommand saveClick => _saveClick;


        public event PropertyChangedEventHandler PropertyChanged;

        protected void PropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public UserCreationPageModelView(){ }

        public UserCreationPageModelView(UserCreationPage userCreationPage)
        {
            _userCreationPage = userCreationPage;
            userService = new UserService();
            _saveClick = new DelegateCommand(save_Click);
        }

        public void save_Click(object e)
        {
            var passwordBox = e as PasswordBox;
            this._password = passwordBox.Password;

            bool userChecked = (bool) _userCreationPage.userCheck.IsChecked;
            bool adminChecked = (bool) _userCreationPage.adminCheck.IsChecked;

            User user = null;

            if(userChecked && !adminChecked)
            {
                user = new User(name, surname, email, phoneNumber, _password, UserRole.USER);
                this.userService.saveUser(user);
            }
            else if(!userChecked && adminChecked)
            {
                user = new User(name, surname, email, phoneNumber, _password, UserRole.ADMIN);
                this.userService.saveUser(user);
            }
            else
            {
                MessageBox.Show("Warning! Please select a role for User.", "Warning!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            MessageBox.Show("User Created \n" + userSavedResult(user), "User Created", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public string userSavedResult(User user)
        {
            return user.name + "\n" + user.surname + "\n" + user.email + "\n" + user.phoneNumber + "\n" + user.userRole;
        }

    }
}
