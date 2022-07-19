using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfApp1.View;
using System.Windows.Controls;
using PromptDialog;
using WpfApp1.Model;
using System.Windows;

namespace WpfApp1.ModelView
{
    public class MainPageModelView
    {

        public MainPage mainPage { get; set; }

        private UserService _userService { get; set; }

        public readonly DelegateCommand _createPatientClick, _patientsListClick, 
                               _createIngredientClick, _ingredientsListClick,
                               _createOrderClick, _orderListClick,
                               _createUserClick;


        public ICommand createPatientClick => _createPatientClick;
        public ICommand patientsListClick => _patientsListClick;

        public ICommand createOrderClick => _createOrderClick;
        public ICommand createIngredientClick => _createIngredientClick;

        public ICommand ingredientsListClick => _ingredientsListClick;
        public ICommand ordersList => _orderListClick;

        public ICommand createUserClick => _createUserClick;

        public MainPageModelView(MainPage mainPage)
        {
            this.mainPage = mainPage;
            _createIngredientClick = new DelegateCommand(createIngredient_Click);
            _createOrderClick = new DelegateCommand(createOrder_Click);
            _createPatientClick = new DelegateCommand(createPatient_Click);
            _patientsListClick = new DelegateCommand(patientsList_Click);
            _ingredientsListClick = new DelegateCommand(ingredientsList_Click);
            _orderListClick = new DelegateCommand(orderList_Click);
            _createUserClick = new DelegateCommand(createUser_Click);
            _userService = new UserService();
            authorize();
        }

        public void createPatient_Click(object e)
        {
            PatientCreatePage patientCreatePage = new PatientCreatePage();
            setMainWindowDataContextAndContent(patientCreatePage);
        }

        public void patientsList_Click(object e)
        {
            PatientList patientList = new PatientList();
            setMainWindowDataContextAndContent(patientList);
        }

        public void createOrder_Click(object e)
        {
            OrderCreationPage orderCreationPage = new OrderCreationPage();
            setMainWindowDataContextAndContent(orderCreationPage);
        }

        public void orderList_Click(object e)
        {
            OrdersList orderList = new OrdersList();
            setMainWindowDataContextAndContent(orderList);
        }

        public void createIngredient_Click(object e)
        {
            IngredientCreate ingredientCreate = new IngredientCreate();
            setMainWindowDataContextAndContent(ingredientCreate);
        }

        public void ingredientsList_Click(object e)
        {
            IngredientListPage ingredientListPage = new IngredientListPage();
            setMainWindowDataContextAndContent(ingredientListPage);
        }

        public void createUser_Click(object e)
        {
            UserCreationPage userCreationPage = new UserCreationPage();
            setMainWindowDataContextAndContent(userCreationPage);
        }

        public MainWindow setMainWindowDataContextAndContent(Page page)
        {
            var mainWindow = getMainWindow();
            mainWindow.Content = page;
            mainWindow.DataContext = page;

            return mainWindow;
        }


        public void authorize()
        {
            User user = Service.AuthService.getLoggedInUser(new Data.UserWPFContext());
            if(user is null)
            {
                MessageBox.Show("Authorization problem", "Authorization failed", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                getMainWindow().Close();
            }

            if (user.userRole.Equals(UserRole.USER))
            {
                mainPage.addIngredientButton.IsEnabled = false;
                mainPage.createUserButton.Visibility = Visibility.Collapsed;
            }

        }

        public MainWindow getMainWindow()
        {
            return this.mainPage.Parent as MainWindow;
        }

    }
}
