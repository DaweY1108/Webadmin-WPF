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
    internal class ProductsViewModel : ViewModelBase
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
        public ICommand DeleteProductCommand { get; }
        public ICommand AddProductCommand { get; }
        public ObservableCollection<Dictionary<string, object>> Products { get; }

        public ProductsViewModel()
        {
            Products = new ObservableCollection<Dictionary<string, object>>();
            RefreshProducts();
            DeleteProductCommand = new ViewModelCommand(ExecuteDeleteProductCommand);
            CloseCommand = new ViewModelCommand(ExecuteCloseCommand);
            MinimizeCommand = new ViewModelCommand(ExecuteMinimizeCommand);
            BackCommand = new ViewModelCommand(ExecuteBackCommand);
            AddProductCommand = new ViewModelCommand(ExecuteAddProductCommand);
        }

        private void ExecuteAddProductCommand(object obj)
        {
            WindowManager.ShowWindow(WindowType.AddProduct);
            IsViewVisible = false;
        }

        private void ExecuteDeleteProductCommand(object obj)
        {
            if (obj is Dictionary<string, object> product)
            {
                MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretné a terméket: " + product["Title"] + "?", "Termék törlése", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int id = (int)product["Id"];
                    ProductModel.DeleteProduct(id);
                    RefreshProducts();
                }
            }
            else
            {
                Console.WriteLine("obj is not of type Dictionary<string, object>");
            }
        }

        private void RefreshProducts()
        {
            Products.Clear();
            IEnumerable<ProductModel.Record> products = ProductModel.GetProducts();
            foreach (ProductModel.Record product in products)
            {
                Dictionary<string, object> productData = new Dictionary<string, object>
                {
                    { "Id", product._id },
                    { "Title", product._title },
                    { "Price", product._price },
                    { "Description", product._description },
                    { "PublishYear", product._publish_year },
                    { "Rating", product._rating },
                    { "Publisher", product._publisher },
                    { "Platform", product._platform }
                };
                Products.Add(productData);
            }
        }

        private void ExecuteBackCommand(object obj)
        {
            WindowManager.ShowWindow(WindowType.Main);
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
