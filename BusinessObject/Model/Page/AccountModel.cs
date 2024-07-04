using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class LoginAccountModel
    {
        /// <summary>
        /// UserName
        /// </summary>
        [MaxLength(50)]
        [MinLength(6)]
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        [MaxLength(32)]
        [MinLength(6)]
        [PasswordPropertyText]
        [Required]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        [MaxLength(32)]
        [MinLength(6)]
        [PasswordPropertyText]
        public string? RePassword { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [MaxLength(32)]
        [MinLength(6)]
        [PasswordPropertyText]
        public string? OldPassword { get; set; }
    }
    public class AccountModel
    {
        /// <summary>
        /// UserName
        /// </summary>
        [MaxLength(50)]
        [MinLength(6)]
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        [MaxLength(32)]
        [MinLength(6)]
        [PasswordPropertyText]
        [Required]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Fullname
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Fullname { get; set; } = string.Empty;

        /// <summary>
        /// PhoneNumber
        /// </summary>
        [Length(11, 11)]
        [Phone]
        [Required]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Email
        /// </summary>
        [MaxLength(255)]
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

    }
}
