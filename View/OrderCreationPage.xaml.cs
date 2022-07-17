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
    /// Interaction logic for OrderCreationPage.xaml
    /// </summary>
    public partial class OrderCreationPage : Page
    {
        OrderCreationModelView orderCreationModelView { get; set; }
        public OrderCreationPage()
        {
            InitializeComponent();
            orderCreationModelView = new OrderCreationModelView(this);
            DataContext = orderCreationModelView;
        }
    }
}
