using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runescape_GrandExchange.Model;
namespace Runescape_GrandExchange.ViewModel
{
    class CategoriesOverviewPageVM
    {
        public CategoriesOverviewPageVM()
        {
            Categories = Repositories.ItemRepository.GetCategories();
        }
        public List<Category> Categories { get; set; }

    }
}
