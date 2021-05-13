using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runescape_GrandExchange.Model;
using Newtonsoft.Json;
using GalaSoft.MvvmLight;
namespace Runescape_GrandExchange.ViewModel
{
    class ItemsDetailPageVM : ViewModelBase
    {
        public ItemsDetailPageVM() { }
        public Item CurrentItem { get { return _item; } set { _item = value;  RaisePropertyChanged(nameof(CurrentItem)); } }
        private Item _item = new Item() { ImageLink= "https://secure.runescape.com/m=itemdb_rs/1620640970174_obj_big.gif?id=12091", Name = "Compost mound pouch", Description= "I can summon a compost mound familiar with this.", CategoryName= "Familiars", CurrentPriceData=new PriceData(){Trend="neutral", Price="850"}, MonthPriceData=new PriceDataChange {Trend="positive", Change="+32.1%" } };
    }
}
