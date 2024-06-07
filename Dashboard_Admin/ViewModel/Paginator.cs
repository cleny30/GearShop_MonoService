using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace WPFStylingTest.ViewModel
{
    public class Paginator<T> : INotifyPropertyChanged
    {
        private ICollectionView _collectionView;
        private int _itemsPerPage;
        private int _currentPage;
        private ObservableCollection<T> _items;
        private string _searchQuery;


        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
                SetupPaging();
            }
        }


        public Paginator(ObservableCollection<T> items)
        {
            _items = items;
            _itemsPerPage = 7;
            _currentPage = 1;
            SetupPaging();
        }

        public ICollectionView CollectionView
        {
            get => _collectionView;
            set
            {
                _collectionView = value;
                OnPropertyChanged(nameof(CollectionView));
            }
        }

        public int CurrentPage
        {

            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                    SetupPaging();
                }
            }
        }

        public ICommand NextPageCommand => new SimpleCommand(NextPage, CanGoNext);
        public ICommand PreviousPageCommand => new SimpleCommand(PreviousPage, CanGoPrevious);

        private void SetupPaging()
        {
            CollectionView = CollectionViewSource.GetDefaultView(_items);
            CollectionView.Filter = FilterItems;
        }


        private bool FilterItems(object obj)
        {
            if (obj is T item)
            {
                var itemType = typeof(T);
                var nameProperty = itemType.GetProperty("Name");

                if (!string.IsNullOrEmpty(SearchQuery))
                {
                    string itemName = nameProperty.GetValue(item)?.ToString() ?? string.Empty;
                    bool matchesSearch = string.IsNullOrEmpty(SearchQuery)||itemName.Contains(SearchQuery, StringComparison.InvariantCultureIgnoreCase);
                    return matchesSearch;
                        
                } else
                {
                    int index = _items.IndexOf(item);
                    return index >= (CurrentPage - 1) * _itemsPerPage && index < CurrentPage * _itemsPerPage;

                }
            }
            return false;
        }

        private void NextPage()
        {
            if (CanGoNext())
            {
                CurrentPage++;
            }
        }

        private bool CanGoNext()
        {
            return (_currentPage * _itemsPerPage) < _items.Count;
        }

        private void PreviousPage()
        {
            if (CanGoPrevious())
            {
                CurrentPage--;
            }
        }

        private bool CanGoPrevious()
        {
            return _currentPage > 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
