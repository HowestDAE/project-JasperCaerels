using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runescape_GrandExchange.Model;
namespace Runescape_GrandExchange.Repositories
{
    interface IGrandExchangeRepository
    {
        public Task<List<Item>> GetItemsAsync();
        public Task<List<Category>> GetCategoriesAsync();
        public Task<List<Item>> GetItemsByCategoryAsync(Category category);
    }
}
