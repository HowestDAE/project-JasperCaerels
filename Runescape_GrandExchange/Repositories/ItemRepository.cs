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
using System.Text.RegularExpressions;
namespace Runescape_GrandExchange.Repositories
{
    class ItemRepository : IGrandExchangeRepository
    {
        private static List<Category> _categories = null;
        private static List<Item> _items = null;
        public async Task<List<Item>> GetItemsAsync()
        {
            if (_items != null)
                return _items;
            _items = new List<Item>();
            int itemNr = 1;
            string jsonFilePath =$"Runescape_GrandExchange.Resources.Data.Items.Item{itemNr}.json";
            string fileVerificationPath = $"../../../Resources/Data/Items/Item{itemNr}.json";
            List<Task<Item>> itemTasks = new List<Task<Item>>();
            FileStream file = File.Open(fileVerificationPath, FileMode.Open);
            while (File.Exists(fileVerificationPath))
            {
                itemTasks.Add(GetItemsFromJsonAsync(jsonFilePath));
                
                itemNr++;
                jsonFilePath = $"Runescape_GrandExchange.Resources.Data.Items.Item{itemNr}.json";
                fileVerificationPath = $"../../../Resources/Data/Items/Item{itemNr}.json";
            }
            await Task.WhenAll(itemTasks);
            foreach(var task in itemTasks)
            {
                if (task.Result != null)
                    _items.Add(task.Result);
            }
            return _items;
        }

        private static async Task<Item> GetItemsFromJsonAsync(string jsonFilePath)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(jsonFilePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    JObject jObject = JObject.Parse(reader.ReadToEnd());
                    Item item = jObject.SelectToken("item").ToObject<Item>();
                    await Task.Delay(100);
                    return item;
                }
            }
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            //categories are ints, the order of int - category is alphabetical, e.g. ammo == 1st category, arrows == 2nd in the list, Miscellaneous is always 0th
            if (_items == null)
                _items = await GetItemsAsync();
            if (_categories != null)
                return _categories;
            _categories = new List<Category>();

            //Get all categorynames from items
            List<firstLetterFilter> itemsPerCategoryInApp = new List<firstLetterFilter>();
            foreach (Item item in _items)
            {

                Category category = new Category();
                category.Name = item.CategoryName;
                _categories.Add(category);
            }
            _categories = _categories.DistinctBy((e) => e.Name).ToList();//filter out duplicates
            _categories = _categories.OrderBy((e) => e.Name).ToList();//sort on Name

            //get the corresponding IDs from a local json file
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string jsonFilePath = $"Runescape_GrandExchange.Resources.Data.categories.json";
            using (Stream stream = assembly.GetManifestResourceStream(jsonFilePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    string jsonData = reader.ReadToEnd();
                    Regex myRegex = new Regex(" ");

                    for (int i = 0; i < _categories.Count; i++)
                    {
                        string underscoreCategoryName = myRegex.Replace(_categories[i].Name, "_");
                        _categories[i].Id = JObject.Parse(jsonData).SelectToken($"{underscoreCategoryName}").ToObject<int>();
                    }

                }
            }

            foreach (Category category in _categories)
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
            List<Task<int>> tasks = new List<Task<int>>();
            for (int i = 0; i < _categories.Count; i++)
            {
                jsonFilePath = $"Runescape_GrandExchange.Resources.Data.ItemsPerCategory.Category{i+1}.json";
                tasks.Add(GetItemsPerCategoryFromJsonAsync(jsonFilePath));
            }
            await Task.WhenAll(tasks);
            int idx=0;
            foreach(var task in tasks)
            {
                _categories[idx].ItemsAmount = task.Result;
                idx++;
            }
            return _categories;
        }

        private static async Task<int> GetItemsPerCategoryFromJsonAsync(string jsonFilePath)
        {
            //READ AMOUNT OF ITEMS/CATEGORY
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
           
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
                    await Task.Delay(100);
                    return totalAmountItems;
                }
            }
        }

        public async Task<List<Item>> GetItemsByCategoryAsync(Category category)
        {
            await Task.Delay(0);
            return _items.FindAll((e) => e.CategoryID == category.Id);
        }

    }
}
