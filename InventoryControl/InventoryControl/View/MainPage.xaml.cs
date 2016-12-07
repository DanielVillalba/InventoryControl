using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InventoryControl
{
    public partial class MainPage : ContentPage
    {

        private AzureClient _client;
        public ObservableCollection<Product> Products { get; set; }
        public Command LoadDirectoryCommand { get; set; }
        public Command DeleteDirectoryCommand { get; set; }
        public Command GenerateProductsCommand { get; set; }
        public MainPage()
        {
            LoadDirectoryCommand = new Command(() => Load());
            DeleteDirectoryCommand = new Command(() => Delete());
            GenerateProductsCommand = new Command(() => Generate());
            Products = new ObservableCollection<Product>();
            _client = new AzureClient();
            InitializeComponent();
            //this.BindingContext = new ProductDirectoryVM();
            lvProducts.ItemSelected += LvProducts_ItemSelected;
        }

        private async void Generate()
        {
            if (IsBusy) return;

            IsBusy = true;
            string[] productName = { "Needle", "Wire stripper and cutter", "Mattock", "Wheelbarrow", "Shears", "Lawnmower", "Screwdriver",
                                    "Stepladder", "Hammer", "Shovel", "Hacksaw"};

            string[] productArea = { "Garden", "Construction", "Home", "School" };

            string[] productBrand = { "Philips", "Makita", "Craftsman", "DeWalt" };

            string[] productLocation = { "Rack A1", "Rack A2", "Rack A3", "Rack B1", "Rack B2", "Rack B3", "Rack C1", "Rack C2", "Rack C3" };

            string[] keys = { "A", "B", "C", "D" };

            Random rdn = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < 3; i++)
            {
                Product product = new Product();
                product.Name = productName[rdn.Next(0, 10)];
                product.UseArea = productArea[rdn.Next(0, 3)];
                product.Brand = productBrand[rdn.Next(0, 3)];
                product.Location = productLocation[rdn.Next(0, 8)];
                product.Qty = rdn.Next(1, 20);
                product.Key = keys[rdn.Next(0, 3)] + DateTime.Now.Millisecond.ToString();
                _client.AddProduct(product);
                Products.Add(product);
            }

            IsBusy = false;
        }

        private async void Delete()
        {
            if (IsBusy) return;

            IsBusy = true;
            await _client.CleanData();
            Products.Clear();
            IsBusy = false;
        }

        private async void Load()
        {
            if (IsBusy) return;

            IsBusy = true;
            var result = await _client.GetProducts();

            Products.Clear();
            foreach (var product in result)
            {
                Products.Add(product);
            }

            IsBusy = false;
        }

        private void LvProducts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Product selectedProduct = (Product)e.SelectedItem;

            if (selectedProduct == null) return;

            Navigation.PushAsync(new View.SelectedProductPage(selectedProduct));
            lvProducts.SelectedItem = null;
        }
    }
}
