using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;
using WpfApp1.View;
using WpfApp1.Service;
using WpfApp1.Model;
using System.Windows;

namespace WpfApp1.ModelView
{
    public class IngredientListModelView : INotifyPropertyChanged
    {


        private IngredientListPage _ingredientListPage { get; set; }

        private IngredientService _ingredientService { get; set; }

        public List<Ingredient> ingredients { get; set; }

        private string _searchText { get; set; }
        public string searchText { get => _searchText; set { this._searchText = value; PropertyChange("searchText"); } }

        public readonly DelegateCommand _createIngredient, _deleteIngredient, _refreshIngredients, _mainPageClick;

        public ICommand createIngredient => _createIngredient;

        public ICommand deleteIngredient => _deleteIngredient;

        public ICommand refreshIngredients => _refreshIngredients;

        public ICommand mainPageClick => _mainPageClick;


        public event PropertyChangedEventHandler PropertyChanged;

        protected void PropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                if (_searchText == null || _searchText.Length == 0 || _searchText == "")
                {
                    _ingredientListPage.ingredientListView.ItemsSource = ingredients;
                   
                }
                this._ingredientListPage.ingredientListView.ItemsSource = ingredients
                                                                   .FindAll(i => i.name.Contains(searchText)).ToList();
            }
        }

        public IngredientListModelView() { }
        public IngredientListModelView(IngredientListPage ingredientListPage)
        {
            _ingredientListPage = ingredientListPage;
            _ingredientService = new IngredientService();
            _createIngredient = new DelegateCommand(createIngredient_Click);
            _deleteIngredient = new DelegateCommand(deleteIngredient_Click);
            _refreshIngredients = new DelegateCommand(refreshIngredients_Click);
            _mainPageClick = new DelegateCommand(mainPage_Click);
            ingredients = _ingredientService.findAll();
            _ingredientListPage.ingredientListView.ItemsSource = ingredients;
            authorize();
        }

        public void createIngredient_Click(object e)
        {
            IngredientCreate ingredientCreate = new IngredientCreate();
            var parent = this._ingredientListPage.Parent as MainWindow;
            parent.Content = ingredientCreate;
            parent.DataContext = ingredientCreate;
        }

        public void deleteIngredient_Click(object e)
        {
            Ingredient selected = this._ingredientListPage.ingredientListView.SelectedItem as Ingredient;
            if(selected is null)
            {
                MessageBox.Show("Please select a item");
            }

            bool deleted = _ingredientService.deleteIngredient(selected);

            if(deleted == true)
            {
                MessageBox.Show("Ingredient " + selected.ToString() + " deleted", "Ingredient deleted", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK); ;
                return;
            }
        }

        public void refreshIngredients_Click(object e)
        {
            ingredients = _ingredientService.refreshIngredients();
            _ingredientListPage.ingredientListView.ItemsSource = ingredients;
        }

        public void mainPage_Click(object e)
        {
            MainPage mainPage = new MainPage();
            var mainWindow = _ingredientListPage.Parent as MainWindow;
            mainWindow.Content = mainPage;
            mainWindow.DataContext = mainPage;
        }

        public void authorize()
        {
            User user = AuthService.getLoggedInUser(new Data.UserWPFContext());

            if (user.userRole.Equals(UserRole.USER))
            {
                _ingredientListPage.addIngredientButton.IsEnabled = false;
                _ingredientListPage.deleteIngredientButton.IsEnabled = false;
            }

        }


    }
}
