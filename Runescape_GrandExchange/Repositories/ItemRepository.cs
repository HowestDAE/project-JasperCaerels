using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runescape_GrandExchange.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
namespace Runescape_GrandExchange.Repositories
{
    class ItemRepository
    {
        private static List<int> _categories = null;
        private static List<string> _categoryNames = null;
        private static List<Item> _items = null;
        public static List<Item> GetItems()
        {
            if (_items != null)
                return _items;
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            int itemNr = 1;
            string jsonFilePath =$"Runescape_GrandExchange.Resources.Data.Items.Item0{itemNr}.json";
            while(File.Exists(jsonFilePath))
            {
                using(Stream stream = assembly.GetManifestResourceStream(jsonFilePath))
                {
                    using(var reader = new StreamReader(stream))
                    {
                        JObject jObject = JObject.Parse(reader.ReadToEnd());
                        _items.Add(jObject.SelectToken("item").ToObject<Item>());
                    }
                }
            }
            return _items;
        }
        public static List<string> GetCategoriesNames()
        {
            //categories are ints, the order of int - category is alphabetical, e.g. ammo == 1st category, arrows == 2nd in the list, Miscellaneous is always 0th
            List<Item> items = GetItems();
            if (_categoryNames != null)
                return _categoryNames;
            foreach (Item item in items)
            {
                _categoryNames.Add(item.Category);
            }
            _categoryNames = _categoryNames.Distinct().ToList();
            return _categoryNames;
        }
        public static List<string> GetItemsByCategory()
        {
            //List<Item>()
            return null;
        }
        public static List<string> GetItemsInCategoryByFirstLetter(string letter)
        {
            return null;
        }
    }
}
