﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
namespace Runescape_GrandExchange.Model
{
    class Item
    {
        public Item() { }

        [JsonIgnore]
        public BitmapImage Image
        {
            get
            {
                return new BitmapImage(new Uri($"https://secure.runescape.com/m=itemdb_rs/1619431351460_obj_big.gif?id={Id}"));
            }
        }
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Category { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "members")]
        public bool Members { get; set; }

        [JsonProperty(PropertyName = "current")]
        public PriceData CurrentPriceData { get; set; }
        [JsonProperty(PropertyName = "day30")]
        public PriceDataChange MonthPriceData { get; set; }

        [JsonIgnore]
        public int PriceChange { get; set; }
        [JsonIgnore]
        public int CurrentPrice { get; set; }
        [JsonIgnore]
        public string PriceTrend { get; set; }
    }

    class PriceData
    {
        [JsonProperty(PropertyName = "trend")]
        public string trend;
        [JsonProperty(PropertyName = "price")]
        public string price;
    }

    class PriceDataChange
    {
        [JsonProperty(PropertyName = "trend")]
        public string trend;
        [JsonProperty(PropertyName = "change")]
        public string change;
    }
}