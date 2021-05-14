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
using Runescape_GrandExchange.ViewModel;
using System.Net.Http;
using System.Text.RegularExpressions;
namespace Runescape_GrandExchange.Repositories
{
    class ItemApiRepository : IGrandExchangeRepository
    {
        public int ItemsLoaded { get; set; }
        private List<Category> _categories = null;
        private HttpClient _client = new HttpClient();
        private static List<Item> _items = null;
        public async Task<List<Item>> GetItemsAsync()
        {
            if (_items != null)
                return _items;
            _items = new List<Item>();
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string jsonFilePath = $"Runescape_GrandExchange.Resources.Data.Items.itemsIDsOnly.json";
            List<int> itemIDs;
            using (Stream stream = assembly.GetManifestResourceStream(jsonFilePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    JObject jObject = JObject.Parse(reader.ReadToEnd());
                    itemIDs = jObject.SelectToken("ids").ToObject<List<int>>();
                }
            }
            //Online API
            List<Task<Item>> tasks = new List<Task<Item>>();
            List<Task<HttpResponseMessage>> responseTasks = new List<Task<HttpResponseMessage>>();
           
                try
                {
                    foreach (int itemID in itemIDs)
                    {
 
                        string endpoint = $"https://secure.runescape.com/m=itemdb_rs/api/catalogue/detail.json?item={itemID}";
                        responseTasks.Add(GetResponseAsync(_client, endpoint));
                    }
                }
                catch (Exception)
                {
                    throw new Exception();
                }
                await Task.WhenAll(responseTasks);

                Regex myRegex = new Regex("<");
                foreach (var response in responseTasks)
                {

                    if (!response.IsFaulted && response.Result.Content.Headers.ContentLength > 0)
                    {
                        if (!myRegex.IsMatch(response.Result.Content.ReadAsStringAsync().Result))
                            tasks.Add(GetItemAsync(response.Result));
                    }
                }

                await Task.WhenAll(tasks);
                foreach (var item in tasks)
                {
                    _items.Add(item.Result);
                }
            
            return _items;
        }

        private static async Task<Item> GetItemAsync(HttpResponseMessage response)
        {
            await Task.Delay(1);
            string json = await response.Content.ReadAsStringAsync();
            Item item = await GetItemFromJsonAsync(json);
            return item;
        }
        private static async Task<Item> GetItemFromJsonAsync(string json)
        {
            await Task.Delay(1);
            Item item = JToken.Parse(json).SelectToken("item").ToObject<Item>();
            return item;
        }
        private static async Task<HttpResponseMessage> GetResponseAsync(HttpClient client, string endpoint)
        {
            await Task.Delay(1);

            var response = await client.GetAsync(endpoint);
            return response;
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

            Regex myRegexHtml = new Regex("<");
            for (int i = 0; i < _categories.Count; i++)
            {
                string endpoint = $"https://secure.runescape.com/m=itemdb_rs/api/catalogue/category.json?category={_categories[i].Id}";
                //await Task.Delay(1000);//to lessen chances on Jagex api servers overloading
                var response = await _client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(response.ReasonPhrase);
                if (response.Content.Headers.ContentLength > 0)
                {
                    if (!myRegexHtml.IsMatch(response.Content.ReadAsStringAsync().Result))
                        tasks.Add(GetAmountOfItemsPerCategoryAsync(response));
                }
            }
            await Task.WhenAll(tasks);

            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Result > 0)
                    _categories[i].ItemsAmount = tasks[i].Result;
            }



            return _categories;
            
        }
        private static async Task<int> GetAmountOfItemsPerCategoryAsync(HttpResponseMessage response)
        {

            string json = await response.Content.ReadAsStringAsync();
            List<firstLetterFilter> itemsInCategoryPerLetter = JObject.Parse(json).SelectToken("alpha").ToObject<List<firstLetterFilter>>();

            //count all the items per letter sum so we have a total items per category
            int totalAmountItems = 0;
            for (int j = 0; j < itemsInCategoryPerLetter.Count; j++)
            {
                totalAmountItems += itemsInCategoryPerLetter[j].ItemsAmount;
            }

            return totalAmountItems;
        }


        public async Task<List<Item>> GetItemsByCategoryAsync(Category category)
        {
            await Task.Delay(0);
            return _items.FindAll((e) => e.CategoryID == category.Id);
        }

    }
}
