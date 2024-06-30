using BusinessObject.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class DeliveryAddressModel
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Fullname { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string? Specific { get; set; } = null!;

        public bool IsDefault { get; set; }

        public virtual Customer UsernameNavigation { get; set; } = null!;
    }
}
