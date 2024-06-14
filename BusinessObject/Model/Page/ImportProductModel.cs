using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class ImportProductModel
    {
        public int ReceiptId { get; set; }

        public DateOnly DateImport { get; set; }

        public string PersonChange { get; set; } = null!;

        public double Payment { get; set; }
    }
}
