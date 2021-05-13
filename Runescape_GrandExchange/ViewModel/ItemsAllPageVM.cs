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
            Items = Repositories.ItemRepository.GetItems();
        }
        public List<Item> Items { get; set; }
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
