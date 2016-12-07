using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace InventoryControl
{
    public partial class App : Application
    {
        //private View.ProductsPage _productsPage;      // download data from azure
        public App()
        {
            //_productsPage = new View.ProductsPage();      // download data from azure

            InitializeComponent();

            //MainPage = _productsPage;     // download data from azure
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            //_productsPage.Load();     // download data from azure
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
