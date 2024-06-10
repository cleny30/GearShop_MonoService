using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class HomeModel
    {
        public List<ProductData> specialSale { get; set; }
        public List<ProductData> mouse { get; set; }
        public List<ProductData> keyboard { get; set; }
    }
}
