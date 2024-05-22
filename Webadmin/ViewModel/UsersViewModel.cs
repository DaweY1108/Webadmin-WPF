using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Webadmin.Data;
using Webadmin.Managers;
using Webadmin.Model;

namespace Webadmin.ViewModel
{
    internal class UsersViewModel : ViewModelBase
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

        public ObservableCollection<Dictionary<string, object>> Users { get; }

        public UsersViewModel()
        {
            Users = new ObservableCollection<Dictionary<string, object>>(); 
            foreach (var _user in UserModel.GetUsers())
            {
                Users.Add(new Dictionary<string, object>
            {
                { "Id", _user._id + " " + (LoggedInUser._user?._username == _user._username ? "(Jelenlegi)" : "")},
                { "Name", _user._first_name + " " + _user._last_name + " (" + _user._username + ")" },
                { "Email", _user._email },
                { "Phone", _user._phone },
                { "Address", _user._country + " " + _user._postal_code + " " + _user._city + " " + _user._street }
            });
            }

            CloseCommand = new ViewModelCommand(ExecuteCloseCommand);
            MinimizeCommand = new ViewModelCommand(ExecuteMinimizeCommand);
            BackCommand = new ViewModelCommand(ExecuteBackCommand);
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
