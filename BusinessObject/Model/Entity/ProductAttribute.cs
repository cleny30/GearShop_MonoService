using System;
using System.Collections.Generic;

namespace BusinessObject.Model.Entity;

public partial class ProductAttribute
{
    public string ProId { get; set; } = null!;

    public string Feature { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual Product Pro { get; set; } = null!;
}
