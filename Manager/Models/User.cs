using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Manager.Models
{
    public class User
    {
        [Key, MaxLength(20), Display(AutoGenerateField = true)]
        public string Username { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        [Required, MaxLength(20)]
        public string Surname { get; set; }

        [Required, MaxLength(50), EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(AutoGenerateField = false)]
        public string Salt { get; set; }

        [Display(AutoGenerateField = false)]
        public string HashedPassword
        {
            get
            {
                System.Security.Cryptography.SHA512Managed sha = new System.Security.Cryptography.SHA512Managed();
                return Convert.ToBase64String(sha.ComputeHash(System.Text.Encoding.Unicode.GetBytes(Username + Salt + Password)));
            }
        }
    }
}