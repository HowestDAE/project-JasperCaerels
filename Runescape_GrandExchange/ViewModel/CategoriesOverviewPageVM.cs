using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runescape_GrandExchange.Model;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight;
namespace Runescape_GrandExchange.ViewModel
{
    class CategoriesOverviewPageVM : ViewModelBase
    {
        public CategoriesOverviewPageVM()
        {
            
        }
        private List<Category> _categories;
        public List<Category> Categories { get {return _categories; } set { _categories = value; RaisePropertyChanged(nameof(Categories)); } }
        private Category _selectedCategory;
        public Category SelectedCategory 
        { 
            get 
            { 
                return _selectedCategory; 
            } 
            set
            {
                _selectedCategory = value;
            } 
        }

     

    }
}
