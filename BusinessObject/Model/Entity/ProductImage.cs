using System;
using System.Collections.Generic;

namespace BusinessObject.Model.Entity;

public partial class ProductImage
{
    public string ProId { get; set; } = null!;

    public string ProImg { get; set; } = null!;

    public virtual Product Pro { get; set; } = null!;
}
