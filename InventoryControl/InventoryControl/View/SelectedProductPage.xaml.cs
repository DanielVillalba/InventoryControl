using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace InventoryControl.View
{
    public partial class SelectedProductPage : ContentPage
    {
        private AzureClient _client;
        private Product product;
        public SelectedProductPage(Product selectedProduct)
        {
            _client = new AzureClient();
            InitializeComponent();
            this.product = selectedProduct;
            this.BindingContext = selectedProduct;
            entryQuantity.Completed += EntryQuantity_Completed;
        }

        private void EntryQuantity_Completed(object sender, EventArgs e)
        {
            Debug.WriteLine("EntryQuantity_Completed");
            _client.UpdateProduct(this.product);
        }
    }
}
