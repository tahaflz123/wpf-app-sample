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
    /// Interaction logic for UserCreationPage.xaml
    /// </summary>
    public partial class UserCreationPage : Page
    {
        public UserCreationPage()
        {
            InitializeComponent();
            UserCreationPageModelView userCreationPageModelView = new UserCreationPageModelView(this);
            DataContext = userCreationPageModelView;
        }
    }
}
