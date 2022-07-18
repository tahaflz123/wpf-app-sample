using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfApp1.Model;
using WpfApp1.ModelView;
using WpfApp1.View;
using WpfApp1.Service;

namespace WpfApp1.ModelView
{
    public class OrdersListModelView
    {

        private OrdersList _ordersListPage { get; set; }

        private OrderService _orderService { get; set; }

        public List<Order> orders { get; set; }

        public readonly DelegateCommand _refreshClick, _mainPageClick;

        public ICommand mainPageClick => _mainPageClick;

        public ICommand refreshClick => _refreshClick;

        public OrdersListModelView() { }
        public OrdersListModelView(OrdersList ordersListPage)
        {
            _ordersListPage = ordersListPage;
            _orderService = new OrderService();
            _mainPageClick = new DelegateCommand(mainPage_Click);
            _refreshClick = new DelegateCommand(refreshPage_Click);
            orders = _orderService.findAll();
            _ordersListPage.ordersListView.ItemsSource = orders;
        }

        public void mainPage_Click(object e)
        {
            MainPage mainPage = new MainPage();
            var mainWindow = _ordersListPage.Parent as MainWindow;
            mainWindow.Content = mainPage;
            mainWindow.DataContext = mainPage;
        }

        public void refreshPage_Click(object e)
        {
            _ordersListPage.ordersListView.ItemsSource = null;
            orders = _orderService.refreshOrders();
            _ordersListPage.ordersListView.ItemsSource = orders;
        }

    }
}
