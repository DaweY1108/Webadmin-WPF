using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Webadmin.Model;
using Webadmin.Data;
using Webadmin.Managers;

namespace Webadmin.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        private bool _isViewVisible = true;
        private string _welcomeMessage;

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

        public string WelcomeMessage
        {
            get
            {
                return _welcomeMessage;
            }

            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand UsersCommand { get; }
        public ICommand OrdersCommand { get; }
        public ICommand ProductsCommand { get; }

        public MainViewModel()
        {
            CloseCommand = new ViewModelCommand(ExecuteCloseCommand);
            MinimizeCommand = new ViewModelCommand(ExecuteMinimizeCommand);
            UsersCommand = new ViewModelCommand(ExecuteUsersCommand);
            OrdersCommand = new ViewModelCommand(ExecuteOrdersCommand);
            ProductsCommand = new ViewModelCommand(ExecuteProductsCommand);
            UserModel.Record _user = LoggedInUser._user;
            WelcomeMessage = "Üdvözöllek, " + _user._first_name  + " " + _user._last_name;
        }

        private void ExecuteProductsCommand(object obj)
        {
            WindowManager.ShowWindow(WindowType.Products);
            IsViewVisible = false;
        }

        private void ExecuteOrdersCommand(object obj)
        {
            WindowManager.ShowWindow(WindowType.Orders);
            IsViewVisible = false;
        }

        private void ExecuteUsersCommand(object obj)
        {        
            WindowManager.ShowWindow(WindowType.Users);
            IsViewVisible = false;
        }

        private void ExecuteMinimizeCommand(object obj)
        {
            WindowManager.MinimizeWindow();
        }

        private void ExecuteCloseCommand(object obj)
        {
            WindowManager.ShutdownApplication();
        }
    }
}
