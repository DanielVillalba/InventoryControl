using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace InventoryControl.View
{
    public partial class ProductsPage : ContentPage
    {
        private AzureClient _client;
        public ObservableCollection<Product> Products { get; set; }
        public Command RefreshCommand { get; set; }
        public ProductsPage()
        {
            RefreshCommand = new Command(() => Load());
            Products = new ObservableCollection<Product>();
            _client = new AzureClient();
            InitializeComponent();
        }

        public async void Load()
        {
            var result = await _client.GetProducts();

            Products.Clear();

            foreach(var product in result)
            {
                Products.Add(product);
            }
        }
    }
}
