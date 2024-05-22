using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Webadmin.Managers;
using Webadmin.Model;

namespace Webadmin.ViewModel
{
    internal class AddProductViewModel : ViewModelBase
    {
        private bool _isViewVisible = true;
        private string _errorMessage;
        private string _productName;
        private string _price;
        private string _description;
        private string _publish_year;
        private string _rating;
        private string _publisher;
        private string _platform;
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

        public string Title
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string PublishYear
        {
            get => _publish_year;
            set
            {
                _publish_year = value;
                OnPropertyChanged(nameof(PublishYear));
            }
        }

        public string Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }

        public string Publisher
        {
            get => _publisher;
            set
            {
                _publisher = value;
                OnPropertyChanged(nameof(Publisher));
            }
        }

        public string Platform
        {
            get => _platform;
            set
            {
                _platform = value;
                OnPropertyChanged(nameof(Platform));
            }
        }

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }

        public ICommand CancelCommand { get; }

        public ICommand AddProductCommand { get; }

        public AddProductViewModel()
        {
            CloseCommand = new ViewModelCommand(ExecuteCloseCommand);
            MinimizeCommand = new ViewModelCommand(ExecuteMinimizeCommand);
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand);
            AddProductCommand = new ViewModelCommand(ExecuteAddProductCommand, CanExecuteAddProductCommand);
        }

        private bool CanExecuteAddProductCommand(object obj)
        {
            if (string.IsNullOrEmpty(Title) || Title == "") return false;
            if (string.IsNullOrEmpty(Price) || Price == "") return false;
            if (string.IsNullOrEmpty(Description) || Description == "") return false;
            if (string.IsNullOrEmpty(PublishYear) || PublishYear == "") return false;
            if (string.IsNullOrEmpty(Rating) || Rating == "") return false;
            if (string.IsNullOrEmpty(Publisher) || Publisher == "") return false;
            if (string.IsNullOrEmpty(Platform) || Platform == "") return false;
            return true;
        }

        private void ExecuteAddProductCommand(object obj)
        {
           if (ProductModel.CheckProduct(Title))
            {
                ErrorMessage = "A termék már létezik!";
                return;
            }
           if (!PublisherModel.CheckPublisher(Publisher))
            {
                PublisherModel.AddPublisher(Publisher);
            }
           if (!PlatformModel.CheckPlatform(Platform))
            {
                 PlatformModel.AddPlatform(Platform);
            }
           PublisherModel.Record publisher = PublisherModel.GetPublisher(Publisher);
           PlatformModel.Record platform = PlatformModel.GetPlatform(Platform);
            decimal _parsedPrice;
            decimal _parsedRating;
            int _parsedPublishYear;
            try
            {
                _parsedPrice = decimal.Parse(Price);
            } catch (Exception e)
            {
                ErrorMessage = "Az ár nem szám!";
                return;
            }
            try
            {
                _parsedRating = decimal.Parse(Rating);
            }
            catch (Exception e)
            {
                ErrorMessage = "Az értékelés nem szám!";
                return;
            }
            try
            {
                _parsedPublishYear = int.Parse(PublishYear);
            }
            catch (Exception e)
            {
                ErrorMessage = "A kiadási év nem szám!";
                return;
            }
            ProductModel.AddProduct(Title, decimal.Parse(Price), Description, int.Parse(PublishYear), decimal.Parse(Rating), publisher._id, platform._id);
            WindowManager.ShowWindow(WindowType.Products);
            IsViewVisible = false;
        }

        private void ExecuteCancelCommand(object obj)
        {
            WindowManager.ShowWindow(WindowType.Products);
            IsViewVisible = false;
        }

        private void ExecuteMinimizeCommand(object obj)
        {
            WindowManager.MinimizeWindow();
        }

        private void ExecuteCloseCommand(object obj)
        {
            WindowManager.CloseWindow();
        }
    }
}
