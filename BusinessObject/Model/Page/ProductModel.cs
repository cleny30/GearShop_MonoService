using BusinessObject.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class ProductModel
    {
        public string ProId { get; set; } = null!;

        public string ProName { get; set; } = null!;

        public int ProQuan { get; set; }

        public double ProPrice { get; set; }

        public int Discount { get; set; }
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public int CateId { get; set; }

        public string CateName { get; set; } = null!;

        public string ProDes { get; set; } = null!;
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