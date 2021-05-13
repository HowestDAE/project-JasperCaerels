using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runescape_GrandExchange.Model;
using GalaSoft.MvvmLight;
namespace Runescape_GrandExchange.ViewModel
{
    class ItemsPerCategoryPageVM : ViewModelBase 
    {
        public ItemsPerCategoryPageVM()
        {
            Repositories.ItemRepository.GetItems();
            Repositories.ItemRepository.GetCategories();
            //Items = Repositories.ItemRepository.GetItemsByCategory(_currentCategory);

        }
        public List<Item> Items { get; set; }
        private Item _selectedItem;
        public Item SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set 
            {
                _selectedItem = value;
            }
        }
        
        private Category _currentCategory;
        public Category CurrentCategory
        {
            get { return _currentCategory; }
            set
            {
                _currentCategory = value;
                Items = Repositories.ItemRepository.GetItemsByCategory(_currentCategory);
                RaisePropertyChanged(nameof(Items));
            }
        }
    }
}
