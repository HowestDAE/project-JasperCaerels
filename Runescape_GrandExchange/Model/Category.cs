using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Runescape_GrandExchange.Model
{
    class Category
    {
        public Category() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public int ItemsAmount { get; set; }
        public int ItemsAmountAvailable { get; set; }

    }
    class firstLetterFilter
    {
        public firstLetterFilter() { }
        [JsonProperty(PropertyName = "letter")]
        public string Letter { get; set; }
        [JsonProperty(PropertyName = "items")]
        public int ItemsAmount { get; set; }

    }
}
