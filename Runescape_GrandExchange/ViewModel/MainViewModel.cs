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
using Runescape_GrandExchange.Repositories;
namespace Runescape_GrandExchange.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private ItemApiRepository _instance = new ItemApiRepository();
        private ItemRepository _localInstance = new ItemRepository();
        public ItemApiRepository Instance { get { return _instance; } }
        public ItemRepository LocalInstance { get { return _localInstance; } }

        private string _loadingScreen;
        public string LoadingScreen 
        { 
            get { return _loadingScreen; }
            set 
            { 
                _loadingScreen = value;
                RaisePropertyChanged("LoadingScreen");
            } 
        }

        public ItemsAllPage ItemsPage { get; set; } = new ItemsAllPage();
        public CategoriesOverviewPage CategoryPage { get; set; } = new CategoriesOverviewPage();
        public ItemDetailPage ItemPage { get; set; } = new ItemDetailPage();
        public ItemsPerCategoryPage ItemsCategoryPage { get; set; } = new ItemsPerCategoryPage();

        public MainViewModel()
        {
            InitializeAllRepositoryData();
        }
        private async Task InitializeAllRepositoryData()
        {

            //Instance.MainViewModelRef = this;//Solely for the sake of showing the data loading

            LoadingScreen = "loading items...";
            (ItemsPage.DataContext as ItemsAllPageVM).Items = await Instance.GetItemsAsync();
            LoadingScreen = "loading categories...";
            (CategoryPage.DataContext as CategoriesOverviewPageVM).Categories = await Instance.GetCategories();
            LoadingScreen = string.Empty;
        }


        private Page _prevPage;
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
        private string _specificButtonContent = "To Selected Item";
        public string SpecificButtonContent { get {return _specificButtonContent; } set {_specificButtonContent = value; RaisePropertyChanged(nameof(SpecificButtonContent)); } }

        private RelayCommand _switchToAllItemsCommand;
        public RelayCommand SwitchToAllItemsCommand { get { if (_switchToAllItemsCommand == null) _switchToAllItemsCommand = new RelayCommand(SwitchToAllItems); return _switchToAllItemsCommand; } }

        private void SwitchToCategories()
        {
            CurrentPage = CategoryPage;
            SpecificButtonContent = "To Selected Category";
        }
        private void SwitchToAllItems()
        {
            CurrentPage = ItemsPage;
            SpecificButtonContent = "To Selected Item";
        }
        private async void SwitchToItem()
        { 
            if(CurrentPage is CategoriesOverviewPage)
            {
                Category category = (CategoryPage.DataContext as CategoriesOverviewPageVM).SelectedCategory;
                if (category == null)
                    return;
                (ItemsCategoryPage.DataContext as ItemsPerCategoryPageVM).CurrentCategory = category;
                (ItemsCategoryPage.DataContext as ItemsPerCategoryPageVM).Items = await Instance.GetItemsByCategory(category);
                CurrentPage = ItemsCategoryPage;
                SpecificButtonContent = "To Selected item";
                
            }
            else if (CurrentPage is ItemsAllPage)
            {
                Item item = (ItemsPage.DataContext as ItemsAllPageVM).SelectedItem;
                if (item == null)
                    return;
                (ItemPage.DataContext as ItemsDetailPageVM).CurrentItem = item;
                _prevPage = CurrentPage;
                CurrentPage = ItemPage;
                SpecificButtonContent = "Back to all items";
            }
            else if(CurrentPage is ItemsPerCategoryPage)
            {
                Item item = (ItemsCategoryPage.DataContext as ItemsPerCategoryPageVM).SelectedItem;
                if (item == null)
                    return;
                (ItemPage.DataContext as ItemsDetailPageVM).CurrentItem = item;
                _prevPage = CurrentPage;
                CurrentPage = ItemPage;
                SpecificButtonContent = $"Back to {item.CategoryName} items";
            }
            else if (CurrentPage is ItemDetailPage)
            {
                CurrentPage = _prevPage;
                SpecificButtonContent = "To Selected item";
            }
        }



    }
}
