using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manager.Models
{
    public class Password
    {
        public Password()
        {
        }

        public Password(string Password)
        {
            Value = Password;
        }

        public string Value { get; set; }

        public User User { get; set; }

        public string Hash
        {
            get
            {
                System.Security.Cryptography.SHA512Managed sha = new System.Security.Cryptography.SHA512Managed();
                return Convert.ToBase64String(sha.ComputeHash(System.Text.Encoding.Unicode.GetBytes(User.Username + User.Salt + Value)));
            }
        }
    }

    public static class PasswordExtension
    {
        public static string HashPassword(this User u)
        {
            return new Password { Value = u.Password, User = u }.Hash;
        }
    }
}