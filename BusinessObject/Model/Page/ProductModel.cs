using BusinessObject.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class ProductModel : INotifyPropertyChanged
    {
        private string proId;
        private string proName;
        private double proPrice;
        private int proQuan;

        private double _totalPrice;

        public double TotalPrice
        {
            get { return ProPrice * ProQuan; }
            set
            {
                // Optional: Adjust ProPrice or ProQuan if TotalPrice is manually set
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    // Implement logic to adjust ProPrice or ProQuan here if necessary
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }
        public int Discount { get; set; }
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public int CateId { get; set; }

        public string CateName { get; set; } = null!;

        public string ProDes { get; set; } = null!;

        public bool IsAvailable { get; set; }

        public string ProId
        {
            get => proId;
            set
            {
                if (proId != value)
                {
                    proId = value;
                    OnPropertyChanged(nameof(ProId));
                }
            }
        }

        public string ProName
        {
            get => proName;
            set
            {
                if (proName != value)
                {
                    proName = value;
                    OnPropertyChanged(nameof(ProName));
                }
            }
        }

        public double ProPrice
        {
            get => proPrice;
            set
            {
                if (proPrice != value)
                {
                    proPrice = value;
                    OnPropertyChanged(nameof(ProPrice));
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }



        public int ProQuan
        {
            get => proQuan;
            set
            {
                if (proQuan != value)
                {
                    proQuan = value;
                    OnPropertyChanged(nameof(ProQuan));
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class ProductData
    {
        public string ProId { get; set; } = null!;

        public int CateId { get; set; }

        public int BrandId { get; set; }

        public string ProName { get; set; } = null!;

        public int ProQuan { get; set; }

        public double ProPrice { get; set; }

        public string ProDes { get; set; } = null!;

        public int Discount { get; set; }

        public bool IsAvailable { get; set; }

        public List<string> ProImg { get; set; } = new List<string>();
        public Dictionary<string, string> ProAttribute { get; set; } = new Dictionary<string, string>();

    }
}