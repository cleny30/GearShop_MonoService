using System;
using System.Collections.Generic;
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
        [MaxLength(250)]
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        [MaxLength(250)]
        [Required]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        [MaxLength(250)]
        public string? RePassword { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [MaxLength(250)]
        public string? OldPassword { get; set; }
    }
    public class AccountModel
    {
        /// <summary>
        /// UserName
        /// </summary>
        [MaxLength(250)]
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        [MaxLength(250)]
        [Required]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Fullname
        /// </summary>
        [MaxLength(500)]
        [Required]
        public string Fullname { get; set; } = string.Empty;

        /// <summary>
        /// PhoneNumber
        /// </summary>
        [MaxLength(11)]
        [Required]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Email
        /// </summary>
        [MaxLength(250)]
        [Required]
        public string Email { get; set; } = string.Empty;

    }
}
