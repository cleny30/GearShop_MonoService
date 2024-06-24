using BusinessObject.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class ManagerModel
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Ssn { get; set; } = null!;

        public string Address { get; set; } = null!;

        public bool IsAdmin { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
