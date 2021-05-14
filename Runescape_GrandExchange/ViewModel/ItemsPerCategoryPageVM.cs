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
        }
        public List<Item> _items = new List<Item>();
        public List<Item> Items { get { return _items; } set { _items = value; RaisePropertyChanged(nameof(Items)); } }
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
                
            }
        }
    }
}
