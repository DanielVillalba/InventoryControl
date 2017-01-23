using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Collections.ObjectModel;

namespace InventoryControl
{
    public class AzureClient
    {
        //GCM
        public MobileServiceClient MobileService { get; set; }

        private IMobileServiceClient _client;
        private IMobileServiceSyncTable<Product> _table;
        const string dbPath = "Inventory";
        const string AzureAccessURL = "http://mypushnotifications.azurewebsites.net";

        public AzureClient()
        {
            _client = new MobileServiceClient(AzureAccessURL);

            //GCM
            MobileService = _client as MobileServiceClient;

            var store = new MobileServiceSQLiteStore(dbPath);
            store.DefineTable<Product>();
            _client.SyncContext.InitializeAsync(store);
            _table = _client.GetSyncTable<Product>();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                await SyncAsync();
            return await _table.ToEnumerableAsync();
        }

        private async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await _client.SyncContext.PushAsync();

                await _table.PullAsync("allProducts", _table.CreateQuery());
            }
            catch(MobileServicePushFailedException pushEx)
            {
                if (pushEx.PushResult != null)
                    syncErrors = pushEx.PushResult.Errors;
            }
        }

        public async void AddProduct(Product product)
        {
            await _table.InsertAsync(product);
        }

        public async Task CleanData()
        {
            await _table.PurgeAsync("allProducts", _table.CreateQuery(), new System.Threading.CancellationToken());
        }

        public async void UpdateProduct(Product product)
        {
            await _table.UpdateAsync(product);
        }
    }
}
