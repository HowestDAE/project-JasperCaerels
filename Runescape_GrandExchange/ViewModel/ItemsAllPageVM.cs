﻿using System;
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
        }
        public List<Item> Items { get; set; }
    }
}
