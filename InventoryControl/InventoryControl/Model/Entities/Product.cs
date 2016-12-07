using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl
{
    [DataTable("Products")]
    public class Product : ObservableBaseObject, IKeyObject
    {
        private string name;
        [JsonProperty("Name")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private string useArea;
        [JsonProperty("UseArea")]
        public string UseArea
        {
            get { return useArea; }
            set { useArea = value; OnPropertyChanged(); }
        }

        private string brand;
        [JsonProperty("Brand")]
        public string Brand
        {
            get { return brand; }
            set { brand = value; OnPropertyChanged(); }
        }

        private string location;
        [JsonProperty("Location")]
        public string Location
        {
            get { return location; }
            set { location = value; OnPropertyChanged(); }
        }

        private int qty;
        [JsonProperty("Qty")]
        public int Qty
        {
            get { return qty; }
            set { qty = value; OnPropertyChanged(); }
        }

        [PrimaryKey]
        [JsonProperty("Id")]
        public string Key { get; set; }
    }
}
