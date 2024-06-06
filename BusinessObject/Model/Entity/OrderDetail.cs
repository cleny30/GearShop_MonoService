using System;
using System.Collections.Generic;

namespace BusinessObject.Model.Entity;

public partial class OrderDetail
{
    public string OrderId { get; set; } = null!;

    public string ProId { get; set; } = null!;

    public string ProName { get; set; } = null!;

    public int Quantity { get; set; }

    public double Price { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Pro { get; set; } = null!;
}
