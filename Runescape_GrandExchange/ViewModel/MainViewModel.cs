using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Runescape_GrandExchange.View;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Controls;
using Runescape_GrandExchange.Model;
namespace Runescape_GrandExchange.ViewModel
{
    class MainViewModel : ViewModelBase
    {
       
        public ItemsAllPage ItemsPage { get; set; } = new ItemsAllPage();
        public CategoriesOverviewPage CategoryPage { get; set; } = new CategoriesOverviewPage();
        public ItemDetailPage ItemPage { get; set; } = new ItemDetailPage();
        public ItemsPerCategoryPage ItemsCategoryPage { get; set; } = new ItemsPerCategoryPage();

        private Page _currentPage;
        public Page CurrentPage
        {
            get
            {
                if (_currentPage == null)
                    _currentPage = ItemsPage;
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                RaisePropertyChanged(nameof(CurrentPage));
            }
        }
        private RelayCommand _switchToCategoryCommand;
        public RelayCommand SwitchToCategoryCommand { get { if (_switchToCategoryCommand == null) _switchToCategoryCommand = new RelayCommand(SwitchToCategories); return _switchToCategoryCommand; } }

        private RelayCommand _switchToItemCommand;
        public RelayCommand SwitchToItemCommand { get { if (_switchToItemCommand == null) _switchToItemCommand = new RelayCommand(SwitchToItem); return _switchToItemCommand; } }

        private RelayCommand _switchToAllItemsCommand;
        public RelayCommand SwitchToAllItemsCommand { get { if (_switchToAllItemsCommand == null) _switchToAllItemsCommand = new RelayCommand(SwitchToAllItems); return _switchToAllItemsCommand; } }

        private void SwitchToCategories()
        {
            CurrentPage = CategoryPage;
        }
        private void SwitchToAllItems()
        {
            CurrentPage = ItemsPage;
        }
        private void SwitchToItem()
        { 
            if(CurrentPage is CategoriesOverviewPage)
            {
                Category category = (CategoryPage.DataContext as CategoriesOverviewPageVM).SelectedCategory;
                (ItemsCategoryPage.DataContext as ItemsPerCategoryPageVM).CurrentCategory = category;
                CurrentPage = ItemsCategoryPage;
            }
            else if (CurrentPage is ItemsAllPage)
            {
                Item item = (ItemsPage.DataContext as ItemsAllPageVM).SelectedItem;
                if (item == null)
                    return;
                (ItemPage.DataContext as ItemsDetailPageVM).CurrentItem = item;
                CurrentPage = ItemPage;
            }
            else if(CurrentPage is ItemsPerCategoryPage)
            {
                Item item = (ItemsCategoryPage.DataContext as ItemsPerCategoryPageVM).SelectedItem;
                if (item == null)
                    return;
                (ItemPage.DataContext as ItemsDetailPageVM).CurrentItem = item;
                CurrentPage = ItemPage;
            }
        }



    }
}
