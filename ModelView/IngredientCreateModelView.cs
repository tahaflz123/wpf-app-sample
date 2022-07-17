using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using WpfApp1.Model;
using WpfApp1.View;
using WpfApp1.Service;

namespace WpfApp1.ModelView
{
    public class IngredientCreateModelView : INotifyPropertyChanged
    {
        private IngredientService _ingredientService { get; set; }
        private string _ingredientName { get; set;}

        public string ingredientName { get => _ingredientName; set { _ingredientName = value; PropertyChange("ingredientName"); } }

        public IngredientCreate ingredientCreateView;

        public readonly DelegateCommand _ingredientSave, _ingredientsListPageClick, _menuPageClick;

        public ICommand saveIngredientClick => _ingredientSave;

        public ICommand ingredientsListPageClick => _ingredientsListPageClick;

        public ICommand menuPageClick => _menuPageClick;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void PropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public IngredientCreateModelView(IngredientCreate ingredientCreateView)
        {
            this.ingredientCreateView = ingredientCreateView;
            this._ingredientSave = new DelegateCommand(ingredientSave_Click);
            _ingredientsListPageClick = new DelegateCommand(ingredientListPage_Click);
            _menuPageClick = new DelegateCommand(menuListPage_Click);
            _ingredientService = new IngredientService();
        }

        public void ingredientSave_Click(object e)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.createdDate = DateTime.Now;
            ingredient.name = ingredientName;
            Ingredient saved = _ingredientService.saveIngredient(ingredient);

            MessageBox.Show(saved.ToString(), "Ingredient Saved!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        public void ingredientListPage_Click(object e)
        {
            IngredientListPage ingredientListPage = new IngredientListPage();
            var mainWindow = this.ingredientCreateView.Parent as MainWindow;
            mainWindow.Content = ingredientListPage;
            mainWindow.DataContext = ingredientListPage;
        }

        public void menuListPage_Click(object e)
        {
            MainPage mainPage = new MainPage();
            var mainWindow = this.ingredientCreateView.Parent as MainWindow;
            mainWindow.Content = mainPage;
            mainWindow.DataContext = mainPage;
        }

    }
}
