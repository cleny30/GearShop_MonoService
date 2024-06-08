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

        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public int CateId { get; set; }

        public string CateName { get; set; } = null!;


    }
}
