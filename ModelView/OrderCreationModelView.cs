using System;
using System.Collections.Generic;
using System.Text;
using WpfApp1.Model;
using WpfApp1.Service;
using WpfApp1.View;
using System.Windows.Input;
using System.Windows;
using PromptDialog;

namespace WpfApp1.ModelView
{
    public class OrderCreationModelView
    {


        private OrderCreationPage _orderCreationPage { get; set; }

        private OrderService _orderService { get; set; }
        private UserService _userService { get; set; }

        private IngredientService _ingredientService { get; set; }

        private IngredientUsageService _ingredientUsageService { get; set; }

        public List<Ingredient> ingredients { get; set; }

        public List<IngredientUsage> ingredientUsages { get; set; }

        public readonly DelegateCommand _deleteIngredientClick, _processOrderClick, _addToOrderClick;

        public ICommand deleteIngredientFromOrderClick => _deleteIngredientClick;

        public ICommand processOrderClick => _processOrderClick;

        public ICommand addToOrderClick => _addToOrderClick;
        
        public OrderCreationModelView() { }
        public OrderCreationModelView(OrderCreationPage orderCreationPage)
        {
            _orderCreationPage = orderCreationPage;
            _orderService = new OrderService();
            _userService = new UserService();
            _ingredientService = new IngredientService();
            _ingredientUsageService = new IngredientUsageService();
            _deleteIngredientClick = new DelegateCommand(deleteIngredient_Click);
            _processOrderClick = new DelegateCommand(processOrder_Click);
            _addToOrderClick = new DelegateCommand(addToOrder_Click);
            ingredients = _ingredientService.findAll();
            orderCreationPage.ingredientListView.ItemsSource = ingredients;
            ingredientUsages = new List<IngredientUsage>();
        }


        public void addToOrder_Click(object e)
        {
            var ingredient = getSelectedIngredient();

            if(ingredient is null)
            {
                return;
            }


            Dialog dialog = new Dialog("mL", "Please give a value", "0", Dialog.InputType.Numero);
            if (dialog.ShowDialog() == true)
            {
                IngredientUsage ingredientUsage = new IngredientUsage(getSelectedIngredient(), null, int.Parse((string) dialog.ResponseText));
                MessageBox.Show("Ingredient added to order,\n" + "mL " + ingredientUsage.usedMl + "\nContained ingredient; " + ingredientUsage.Ingredient.ToString(), "Ingredient added!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                ingredientUsages.Add(ingredientUsage);
                _orderCreationPage.ingredientUsagesList.ItemsSource = null;
                _orderCreationPage.ingredientUsagesList.ItemsSource = ingredientUsages;
                dialog.Close();
            }


        }

        public void deleteIngredient_Click(object e)
        {
            IngredientUsage ingredientUsage = getSelectedIngredientUsage();
            if(ingredientUsage == null)
            {
                return;
            }

            MessageBoxResult result = MessageBox.Show("Ingredient will remove from Order \n" + ingredientUsage.ToString(), "Warning!", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (result.Equals(MessageBoxResult.Cancel))
            {
                return;
            }

            ingredientUsages.Remove(ingredientUsage);
            
            MessageBox.Show("Ingredient removed from order, \n" + "mL" + ingredientUsage.usedMl + "\n Contained ingredient; " + ingredientUsage.Ingredient.ToString()
                , "Ingredient removed", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

            _orderCreationPage.ingredientUsagesList.ItemsSource = null;
            _orderCreationPage.ingredientUsagesList.ItemsSource = ingredientUsages;
        }

        public void processOrder_Click(object e)
        {
            
        }

        public Ingredient getSelectedIngredient()
        {
            var ingredient = _orderCreationPage.ingredientListView.SelectedItem as Ingredient;
            return ingredient;
        }

        public IngredientUsage getSelectedIngredientUsage()
        {
            return _orderCreationPage.ingredientUsagesList.SelectedItem as IngredientUsage;
        }

    }
}
