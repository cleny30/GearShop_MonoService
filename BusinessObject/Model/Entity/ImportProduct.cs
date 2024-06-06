using System;
using System.Collections.Generic;

namespace BusinessObject.Model.Entity;

public partial class ImportProduct
{
    public int ReceiptId { get; set; }

    public DateOnly DateImport { get; set; }

    public string PersonChange { get; set; } = null!;

    public double Payment { get; set; }
}
