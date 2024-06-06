using BusinessObject.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class Product
    {
        public string ProId { get; set; } = null!;

        public string ProName { get; set; } = null!;

        public int ProQuan { get; set; }

        public double ProPrice { get; set; }

        
    }
}
