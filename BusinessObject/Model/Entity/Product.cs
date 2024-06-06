using System;
using System.Collections.Generic;

namespace BusinessObject.Model.Entity;

public partial class Product
{
    public string ProId { get; set; } = null!;

    public int CateId { get; set; }

    public int BrandId { get; set; }

    public string ProName { get; set; } = null!;

    public int ProQuan { get; set; }

    public double ProPrice { get; set; }

    public string ProDes { get; set; } = null!;

    public int Discount { get; set; }

    public bool IsAvailable { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Cate { get; set; } = null!;
}
