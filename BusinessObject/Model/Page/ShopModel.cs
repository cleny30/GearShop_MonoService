using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class ShopModel
    {
        public List<ProductData>? product { get; set; }
        public int saleAmount { get; set; }
    }
}
