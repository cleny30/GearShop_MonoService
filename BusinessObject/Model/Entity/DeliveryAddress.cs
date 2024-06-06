using System;
using System.Collections.Generic;

namespace BusinessObject.Model.Entity;

public partial class DeliveryAddress
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public bool IsDefault { get; set; }

    public virtual Customer UsernameNavigation { get; set; } = null!;
}
