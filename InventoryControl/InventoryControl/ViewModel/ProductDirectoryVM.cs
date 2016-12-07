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
    public class ProductDirectoryVM : ObservableBaseObject
    {
        public ObservableCollection<Product> Products { get; set; }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value;  OnPropertyChanged(); }
        }

        public Command LoadDirectoryCommand { get; set; }
        public Command DeleteDirectoryCommand { get; set; }

        public ProductDirectoryVM()
        {
            Products = new ObservableCollection<Product>();
            IsBusy = false;
            LoadDirectoryCommand = new Command(() => LoadDirectory());
            DeleteDirectoryCommand = new Command(() => DeleteDirectory());
        }

        private async void DeleteDirectory()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                await Task.Delay(3000);

                ProductDirectoryService.ClearProductDirectory();

                Products.Clear();

                IsBusy = false;
            }
        }

        private async void LoadDirectory()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                await Task.Delay(3000);

                ProductDirectory loadedDirectory = ProductDirectoryService.LoadProductDirectory();

                foreach(Product product in loadedDirectory.Products)
                {
                    Products.Add(product);
                }
                IsBusy = false;
            }
        }

        public async void UpdateElement(Product product)
        {
            Debug.WriteLine("UpdateElement ()");
            if (!IsBusy)
            {
                IsBusy = true;
                await Task.Delay(3000);

                ProductDirectoryService.UpdateProduct(product);

                IsBusy = false;
            }
        }


    }
}
