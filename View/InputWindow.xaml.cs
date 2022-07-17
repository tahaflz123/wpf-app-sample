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
using System.Windows.Shapes;
using WpfApp1.ModelView;
namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public InputWindowModelView inputWindowModelView { get; set; }
        public InputWindow()
        {
            InitializeComponent();
            inputWindowModelView = new InputWindowModelView(this);
            DataContext = inputWindowModelView;
        }
    }
}
