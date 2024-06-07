using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;


namespace WPFStylingTest.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public Paginator<Items> ItemsPaginator { get; private set; }
        public Paginator<Items> OtherItemsPaginator { get; private set; }

        public MainViewModel()
        {
            LoadItems();
            LoadOtherItems();
            ItemsPaginator = new Paginator<Items>(Items);
            OtherItemsPaginator = new Paginator<Items>(OtherItems);
        }

        public ObservableCollection<Items> Items { get; set; } = new ObservableCollection<Items>();
        public ObservableCollection<Items> OtherItems { get; set; } = new ObservableCollection<Items>();

        private void LoadItems()
        {
            // Load your Items here. This is just an example.
            Random random = new Random();
            for (int i = 1; i <= 100; i++)
            {
                Items.Add(new Items
                {
                    ID = i,
                    Name = $"Item {i}",
                    Category = $"Category {i % 10}",
                    Brand = $"Brand {i % 5}",
                    Quantity = random.Next(0, 6) // Random number between 0 and 5 (inclusive)
                });
            }
        }

        private void LoadOtherItems()
        {
            // Load your OtherItems here. This is just an example.
            Random random = new Random();
            for (int i = 1; i <= 50; i++)
            {
                OtherItems.Add(new Items
                {
                    ID = i,
                    Name = $"Other Item {i}",
                    Category = $"Category {i % 10}",
                    Brand = $"Brand {i % 5}",
                    Quantity = 0 // Random price between 1 and 100
                });
            }

            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

