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
            _items = new List<Item>();
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            int itemNr = 1;
            string jsonFilePath =$"Runescape_GrandExchange.Resources.Data.Items.Item0{itemNr}.json";
            string fileVerificationPath = $"../../../Resources/Data/Items/Item0{itemNr}.json";
            FileStream file = File.Open(fileVerificationPath, FileMode.Open);
            while (File.Exists(fileVerificationPath))
            {
                using(Stream stream = assembly.GetManifestResourceStream(jsonFilePath))
                {
                    using(var reader = new StreamReader(stream))
                    {
                        JObject jObject = JObject.Parse(reader.ReadToEnd());
                        Item item = jObject.SelectToken("item").ToObject<Item>();
                        _items.Add(item);
                    }
                }
                itemNr++;
                jsonFilePath = $"Runescape_GrandExchange.Resources.Data.Items.Item0{itemNr}.json";
                fileVerificationPath = $"../../../Resources/Data/Items/Item0{itemNr}.json";
            }
            return _items;
        }
        public static List<string> GetCategoryNames()
        {
            //categories are ints, the order of int - category is alphabetical, e.g. ammo == 1st category, arrows == 2nd in the list, Miscellaneous is always 0th
            List<Item> items = GetItems();
            if (_categoryNames != null)
                return _categoryNames;
            _categoryNames = new List<string>();
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
