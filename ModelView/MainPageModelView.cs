using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfApp1.View;
using System.Windows.Controls;

namespace WpfApp1.ModelView
{
    public class MainPageModelView
    {

        public MainPage mainPage { get; set; }

        public readonly DelegateCommand _createPatientClick, _patientsListClick, 
                               _createIngredientClick, _ingredientsListClick,
                               _createOrderClick;

        public ICommand createPatientClick => _createPatientClick;
        public ICommand patientsListClick => _patientsListClick;

        public ICommand createOrderClick => _createOrderClick;
        public ICommand createIngredientClick => _createIngredientClick;

        public ICommand ingredientsListClick => _ingredientsListClick;

        public MainPageModelView(MainPage mainPage)
        {
            this.mainPage = mainPage;
            _createIngredientClick = new DelegateCommand(createIngredient_Click);
            _createOrderClick = new DelegateCommand(createOrder_Click);
            _createPatientClick = new DelegateCommand(createPatient_Click);
            _patientsListClick = new DelegateCommand(patientsList_Click);
            _ingredientsListClick = new DelegateCommand(ingredientsList_Click);
        }

        public void createPatient_Click(object e)
        {
            PatientCreatePage patientCreatePage = new PatientCreatePage();
            var mainWindow = getMainWindow();
            setMainWindowDataContextAndContent(mainWindow, patientCreatePage);

        }

        public void patientsList_Click(object e)
        {
            PatientList patientList = new PatientList();
            var mainWindow = getMainWindow();
            setMainWindowDataContextAndContent(mainWindow, patientList);


        }

        public void createOrder_Click(object e)
        {
            OrderCreationPage orderCreationPage = new OrderCreationPage();
            var mainWindow = getMainWindow();
            setMainWindowDataContextAndContent(mainWindow, orderCreationPage);
        }

        public void createIngredient_Click(object e)
        {
            IngredientCreate ingredientCreate = new IngredientCreate();
            var mainWindow = getMainWindow();
            setMainWindowDataContextAndContent(mainWindow, ingredientCreate);

        }

        public void ingredientsList_Click(object e)
        {
            IngredientListPage ingredientListPage = new IngredientListPage();
            var mainWindow = getMainWindow();
            setMainWindowDataContextAndContent(mainWindow, ingredientListPage);

        }

        public MainWindow setMainWindowDataContextAndContent(MainWindow mainWindow, Page page)
        {
            mainWindow.Content = page;
            mainWindow.DataContext = page;

            return mainWindow;
        }


        public MainWindow getMainWindow()
        {
            return this.mainPage.Parent as MainWindow;
        }

    }
}
