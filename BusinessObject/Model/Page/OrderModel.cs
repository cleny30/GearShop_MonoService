using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class OrderModel
    {
        public string OrderId { get; set; } = null!;

        public int? ManagerId { get; set; }

        public string Username { get; set; } = null!;

        public double TotalPrice { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? OrderDes { get; set; }

        public int Status { get; set; }

        public string Address { get; set; } = null!;
    }
}
