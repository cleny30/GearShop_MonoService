using System;
using System.Collections.Generic;

namespace BusinessObject.Model.Entity;

public partial class Brand
{
    public int BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public string BrandLogo { get; set; } = null!;

    public bool IsAvailable { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
