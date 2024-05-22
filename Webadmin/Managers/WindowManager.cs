using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Webadmin.View;

namespace Webadmin.Managers
{
    public class WindowManager
    {
        public static void ShowWindow(WindowType windowType)
        {
            switch (windowType)
            {
            case WindowType.Login:
                {
                    LoginView loginView = new LoginView();
                    Application.Current.MainWindow = loginView;
                    loginView.IsVisibleChanged += (sender, e) =>
                    {
                        if (loginView.IsVisible == false && loginView.IsLoaded)
                        {
                            try { loginView.Close(); }
                            catch (Exception) { }
                        }
                    };
                    loginView.Show();
                    break;
                }        
            case WindowType.Main:
                {
                    MainView mainView = new MainView();
                    Application.Current.MainWindow = mainView;
                    mainView.IsVisibleChanged += (sender, e) =>
                    {
                        if (mainView.IsVisible == false && mainView.IsLoaded)
                        {
                            mainView.Close();
                        }
                    };
                    mainView.Show();
                    break;
                }
            case WindowType.Products:
                {
                    ProductsView productsView = new ProductsView();
                    Application.Current.MainWindow = productsView;
                    productsView.IsVisibleChanged += (sender, e) =>
                    {
                        if (productsView.IsVisible == false && productsView.IsLoaded)
                        {
                            productsView.Close();
                        }
                    };  
                    productsView.Show();
                    break;
                }
            case WindowType.Orders: 
                {
                    OrdersView ordersView = new OrdersView();
                    Application.Current.MainWindow = ordersView;
                    ordersView.IsVisibleChanged += (sender, e) =>
                    {
                        if (ordersView.IsVisible == false && ordersView.IsLoaded)
                        {
                            ordersView.Close();
                        }
                    };
                    ordersView.Show();
                    break;
                }
            case WindowType.Users:
                {
                    UsersView usersView = new UsersView();
                    Application.Current.MainWindow = usersView;
                    usersView.IsVisibleChanged += (sender, e) =>
                    {
                        if (usersView.IsVisible == false && usersView.IsLoaded)
                    {
                            usersView.Close();
                        }
                    };
                    usersView.Show();
                    break;
                }
            case WindowType.AddProduct:
                {
                AddProductView addProductView = new AddProductView();
                Application.Current.MainWindow = addProductView;
                addProductView.IsVisibleChanged += (sender, e) =>
                {
                    if (addProductView.IsVisible == false && addProductView.IsLoaded)
                    {
                        addProductView.Close();
                    }
                };
                addProductView.Show();
                break;
            }
            default:
                {
                    throw new ArgumentException("Invalid window type");
                }
            }
        }

        public static void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        public static void CloseWindow()
        {
            Application.Current.MainWindow.Close();
        }

        public static void ShutdownApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
