using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runescape_GrandExchange.Model;
namespace Runescape_GrandExchange.ViewModel
{
    class ItemsAllPageVM
    {
        public ItemsAllPageVM() 
        {
            Items = Repositories.ItemRepository.GetItems();
            Categories = Repositories.ItemRepository.GetCategoryNames();
        }
        public List<Item> Items { get; set; }
        public List<string> Categories { get; set; }
    }
}
