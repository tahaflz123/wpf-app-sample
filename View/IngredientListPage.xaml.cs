using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ModelView;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for IngredientListPage.xaml
    /// </summary>
    public partial class IngredientListPage : Page
    {
        public IngredientListPage()
        {
            InitializeComponent();
            IngredientListModelView ingredientListModelView = new IngredientListModelView(this);
            this.DataContext = ingredientListModelView;
        }
    }
}
