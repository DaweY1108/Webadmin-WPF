using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Webadmin.Data;
using Webadmin.Managers;
using Webadmin.Model;
using Webadmin.View;

namespace Webadmin.ViewModel
{
    internal class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

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

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            CloseCommand = new ViewModelCommand(ExecuteCloseCommand);
            MinimizeCommand = new ViewModelCommand(ExecuteMinimizeCommand);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || string.IsNullOrWhiteSpace(Password) || Password.Length < 3) return false;
            else return true;
        }

        private void ExecuteLoginCommand(object obj)
        {
            if (UserModel.CheckUser(Username, Password))
            {
                LoggedInUser._user = UserModel.GetUser(Username);
                WindowManager.ShowWindow(WindowType.Main);
                IsViewVisible = false;
            } else
            {
                ErrorMessage = "Hibás felhasználónév vagy jelszó!";
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
    }
}