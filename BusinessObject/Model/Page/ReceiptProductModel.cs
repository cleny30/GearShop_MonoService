using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class ReceiptProductModel
    {
        public int ReceiptId { get; set; }

        public string ProId { get; set; } = null!;

        public string ProName { get; set; } = null!;

        public int Amount { get; set; }

        public double Price { get; set; }
    }
}
