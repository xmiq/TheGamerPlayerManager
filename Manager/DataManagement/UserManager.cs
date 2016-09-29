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

        public void GetSalt(ref User u)
        {
            var username = mgr.GetParameter();
            username.ParameterName = "@username";
            username.Value = u.Username;

            u.Salt = mgr.Scalar("Login.usp_GetSalt", username).ToString();
        }

        public LoginResult Login(User u)
        {
            SHA512Managed sha = new SHA512Managed();
            GetSalt(ref u);
            string hashedPassword = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(u.Username + u.Salt + u.Password)));

            var username = mgr.GetParameter();
            username.ParameterName = "@Username";
            username.Value = u.Username;

            var password = mgr.GetParameter();
            password.ParameterName = "@Password";
            password.Value = hashedPassword;

            return (LoginResult)mgr.Scalar("Login.usp_Login", username, password);
        }
    }
}