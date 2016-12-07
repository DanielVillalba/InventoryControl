using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace InventoryControl
{
    public interface IKeyObject
    {
        string Key { get; set; }
    }
    public class DBManager
    {
        SQLiteConnection database;

        public DBManager()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Product>();
        }

        public void SaveValue <T> (T Value)
            where T:IKeyObject, new()
        {
            var all = (from entry in database.Table<T>().AsEnumerable<T>()
                       where entry.Key == Value.Key
                       select entry).ToList();
            if (all.Count == 0)
                database.Insert(Value);
            else
                database.Update(Value);
        }

        public void DeleteValue<T>(T Value)
            where T : IKeyObject, new()
        {
            var all = (from entry in database.Table<T>().AsEnumerable<T>()
                       where entry.Key == Value.Key
                       select entry).ToList();
            if (all.Count == 1)
                database.Delete(Value);
            else
                throw new Exception("The data base does not contain an entry with the specified key");
        }

        public List<T> GetAllItems<T>()
            where T : IKeyObject, new()
        {
            return database.Table<T>().AsEnumerable<T>().ToList();
        }

        public T GetItem<T>(string key)
            where T:IKeyObject, new()
        {
            T result = (from entry in database.Table<T>().AsEnumerable<T>()
                        where entry.Key == key
                        select entry).FirstOrDefault<T>();
            return result;
        }

        public void DeleteTable()
        {
            database.DropTable<Product>();
        }
    }
}
