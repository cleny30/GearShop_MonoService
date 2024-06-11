using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class ProductDetailModel
    {
        public ProductData productData {  get; set; }
        public List<ProductData> products { get; set; }
    }
}
