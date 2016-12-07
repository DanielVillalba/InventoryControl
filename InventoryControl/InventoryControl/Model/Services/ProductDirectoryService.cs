using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl
{
    public class ProductDirectoryService
    {
        public static void ClearProductDirectory()
        {
            DBManager dbmanager = new DBManager();
            ObservableCollection<Product> products =  new ObservableCollection<Product>(dbmanager.GetAllItems<Product>());

            if(products.Any())
            {
                dbmanager.DeleteTable();
            }
        }

        public static ProductDirectory LoadProductDirectory()
        {
            DBManager dbmanager = new DBManager();
            ObservableCollection<Product> products = new ObservableCollection<Product>(dbmanager.GetAllItems<Product>());
            ProductDirectory productDirectory = new ProductDirectory();

            if (products.Any())
            {
                productDirectory.Products = products;
                return productDirectory;
            }

            products = new ObservableCollection<Product>();

            string[] productName = { "Needle", "Wire stripper and cutter", "Mattock", "Wheelbarrow", "Shears", "Lawnmower", "Screwdriver",
                                    "Stepladder", "Hammer", "Shovel", "Hacksaw"};

            string[] productArea = { "Garden", "Construction", "Home", "School"};

            string[] productBrand = { "Philips", "Makita", "Craftsman", "DeWalt" };

            string[] productLocation = { "Rack A1", "Rack A2", "Rack A3", "Rack B1", "Rack B2", "Rack B3", "Rack C1", "Rack C2", "Rack C3" };

            string[] keys = { "A","B","C","D"};

            Random rdn = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < 20; i++)
            {
                Product product = new Product();
                product.Name = productName[rdn.Next(0, 10)];
                product.UseArea = productArea[rdn.Next(0, 3)];
                product.Brand = productBrand[rdn.Next(0, 3)];
                product.Location = productLocation[rdn.Next(0, 8)];
                product.Qty = rdn.Next(1, 20);
                product.Key = keys[rdn.Next(0, 3)] + i.ToString();
                products.Add(product);
                dbmanager.SaveValue<Product>(product);
            }
            productDirectory.Products = products;
            return productDirectory;
        }

        public static void UpdateProduct(Product product)
        {
            Debug.WriteLine("UpdateProduct()");
            DBManager dbmanager = new DBManager();
            Product localProduct = new Product();
            localProduct.Name = product.Name;
            localProduct.UseArea = product.UseArea;
            localProduct.Brand = product.Brand;
            localProduct.Location = product.Location;
            localProduct.Qty = product.Qty;
            localProduct.Key = product.Key;
            dbmanager.SaveValue<Product>(localProduct);
        }
    }
}
