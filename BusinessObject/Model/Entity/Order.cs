using System;
using System.Collections.Generic;

namespace BusinessObject.Model.Entity;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public int? ManagerId { get; set; }

    public string Username { get; set; } = null!;

    public double TotalPrice { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? OrderDes { get; set; }

    public int Status { get; set; }

    public string Fullname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual Manager? Manager { get; set; }

    public virtual Customer UsernameNavigation { get; set; } = null!;
}
