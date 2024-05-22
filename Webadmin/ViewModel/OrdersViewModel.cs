using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Webadmin.Data;
using Webadmin.Managers;
using Webadmin.Model;

namespace Webadmin.ViewModel
{
    internal class OrdersViewModel : ViewModelBase
    {
        private bool _isViewVisible = true;
        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }
        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand UserData { get; }
        public ICommand DeleteOrderCommand { get; }
        public ObservableCollection<Dictionary<string, object>> Orders { get; }

        public OrdersViewModel()
        {
            Orders = new ObservableCollection<Dictionary<string, object>>();
            RefreshOrders();   
            DeleteOrderCommand = new ViewModelCommand(ExecuteDeleteOrderCommand);
            CloseCommand = new ViewModelCommand(ExecuteCloseCommand);
            MinimizeCommand = new ViewModelCommand(ExecuteMinimizeCommand);
            BackCommand = new ViewModelCommand(ExecuteBackCommand);
            
        }

        private void ExecuteDeleteOrderCommand(object obj)
        {

            if (obj is Dictionary<string, object> order)
            {
                MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretné a rendelést: " + order["Title"] + "?", "Rendelés törlése", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int id = (int)order["Id"];
                    OrderModel.DeleteOrder(id);
                    RefreshOrders();
                }
            }
            else
            {
                Console.WriteLine("obj is not of type Dictionary<string, object>");
            }
        }

        private void RefreshOrders()
        {
            Orders.Clear();
            foreach (var _order in OrderModel.GetOrders())
            {
                Orders.Add(new Dictionary<string, object>
                {
                    { "Id", _order._id},
                    { "Date", _order._date},
                    { "Title", _order._title + " (" + _order._amount + "db)"},
                    { "Amount", _order._amount},
                    { "State", _order._state},
                    { "Price", _order._price},
                    { "Name", _order._first_name + " " + _order._last_name},
                    { "Phone", _order._phone},
                    { "Email", _order._email},
                    { "Address", _order._country + " " + _order._postal_code + " " + _order._city + " " + _order._street }
                });

            }
        }

        private void ExecuteMinimizeCommand(object obj)
        {
            WindowManager.MinimizeWindow();
        }

        private void ExecuteCloseCommand(object obj)
        {
            WindowManager.ShutdownApplication();
        }

        private void ExecuteBackCommand(object obj)
        {  
            WindowManager.ShowWindow(WindowType.Main);
            IsViewVisible = false;
        }
    }
}
