using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runescape_GrandExchange.Model;
using GalaSoft.MvvmLight;
namespace Runescape_GrandExchange.ViewModel
{
    class ItemsAllPageVM : ViewModelBase
    {
        public ItemsAllPageVM() 
        {
        }
        private List<Item> _items;
        public List<Item> Items { get { return _items; } set { _items = value; RaisePropertyChanged(nameof(Items)); } }
        private Item _selectedItem;

        public Item SelectedItem
        {
            get { return _selectedItem; }
            set 
            {
                _selectedItem = value; 
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

    }
}
