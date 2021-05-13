using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using Runescape_GrandExchange.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
namespace Runescape_GrandExchange.Repositories
{
    class ItemRepository
    {
        private static List<Category> _categories = null;
        private static List<Item> _items = null;
        public static List<Item> GetItems()
        {
            if (_items != null)
                return _items;
            _items = new List<Item>();
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            int itemNr = 1;
            string jsonFilePath =$"Runescape_GrandExchange.Resources.Data.Items.Item{itemNr}.json";
            string fileVerificationPath = $"../../../Resources/Data/Items/Item{itemNr}.json";
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
                jsonFilePath = $"Runescape_GrandExchange.Resources.Data.Items.Item{itemNr}.json";
                fileVerificationPath = $"../../../Resources/Data/Items/Item{itemNr}.json";
            }
            return _items;
        }
        public static List<Category> GetCategories()
        {
            //categories are ints, the order of int - category is alphabetical, e.g. ammo == 1st category, arrows == 2nd in the list, Miscellaneous is always 0th
            List<Item> items = GetItems();
            if (_categories != null)
                return _categories;
            _categories = new List<Category>();
            List<firstLetterFilter> itemsPerCategoryInApp = new List<firstLetterFilter>();
            foreach (Item item in items)
            {
                Category category = new Category();
                category.Name = item.CategoryName;
                _categories.Add(category);
            } 
            _categories = _categories.DistinctBy((e) => e.Name).ToList();
            _categories = _categories.OrderBy((e) => e.Name).ToList();//sort on Name
            Category miscCat = _categories.Find((e) => e.Name == "Miscellaneous");//find category not on alphabetical order

            if(miscCat != null) //if misc category is used
            {
                _categories.Remove(miscCat);//take out misc category
                miscCat.Id = 0; //Change its id to 0
                _categories.Insert(0, miscCat); //add it back to the start of the list
                for (int i = 1; i < _categories.Count; i++)
                {
                    _categories[i].Id = i + 1;
                }
            }
            else
            {
                for (int i = 0; i < _categories.Count; i++)
                {
                    _categories[i].Id = i + 1;
                }
            }
            foreach(Category category in _categories)
            {
                foreach (Item item in _items)
                {
                    if (category.Name.Equals(item.CategoryName))
                    {
                        item.CategoryID = category.Id;
                        category.ItemsAmountAvailable++;
                    }
                }
            }

            //READ AMOUNT OF ITEMS/CATEGORY
            for (int i = 0; i < _categories.Count; i++)
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                string jsonFilePath = $"Runescape_GrandExchange.Resources.Data.ItemsPerCategory.Category{_categories[i].Id}.json";
                using (Stream stream = assembly.GetManifestResourceStream(jsonFilePath))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        JObject jObject = JObject.Parse(reader.ReadToEnd());
                        List<firstLetterFilter> itemsInCategoryPerLetter = jObject.SelectToken("alpha").ToObject<List<firstLetterFilter>>();
                        //count all the items per letter sum so we have a total items per category
                        int totalAmountItems = 0;
                        for (int j = 0; j < itemsInCategoryPerLetter.Count; j++)
                        {
                            totalAmountItems += itemsInCategoryPerLetter[j].ItemsAmount;
                        }
                        _categories[i].ItemsAmount = totalAmountItems;
                    }
                }
                
            }
            return _categories;
        }
        public static List<Item> GetItemsByCategory(Category category)
        {   
            return _items.FindAll((e) => e.CategoryID == category.Id);
        }

        public static List<string> GetItemsInCategoryByFirstLetter(string letter)
        {
            return null;
        }
    }
}
