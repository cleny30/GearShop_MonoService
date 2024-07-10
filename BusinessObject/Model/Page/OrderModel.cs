using BusinessObject.Model.Entity;
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

        public string Fullname { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Email { get; set; } = null!;

        public virtual Manager? Manager { get; set; }

        public virtual Customer UsernameNavigation { get; set; } = null!;

        public string? proId { get; set; }
    }
    public class OrderDataModel
    {
        public string? OrderId { get; set; }
        public string? UserName { get; set; } 
        public double TotalPrice { get; set; }
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly? EndDate { get; set; }
        public string? OrderDes { get; set; } 
        public int Status { get; set; } = 0;
        public string? Address { get; set; } 
        public List<OrderDetailModel>? orderDetail { get; set; }
    }
}
