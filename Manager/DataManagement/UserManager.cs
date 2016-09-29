using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Manager.DataManagement
{
    public class UserManager
    {
        private DataManager mgr = new DataManager();

        public string GetSalt(string Username)
        {
            var username = mgr.GetParameter();
            username.ParameterName = "@username";
            username.Value = Username;

            return mgr.Scalar("Login.usp_GetSalt", username).ToString();
        }

        public bool Login(User u)
        {
            SHA512Managed sha = new SHA512Managed();
            string salt = GetSalt(u.Username);
            string hashedPassword = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(u.Username + salt + u.Password)));
            return false;
        }
    }
}