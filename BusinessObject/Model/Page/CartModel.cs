using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class CartModel
    {
        public string Username { get; set; } = null!;

        public string ProId { get; set; } = null!;

        public string ProName { get; set; } = null!;

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
