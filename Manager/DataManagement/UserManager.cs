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
            GetSalt(ref u);

            var username = mgr.GetParameter();
            username.ParameterName = "@Username";
            username.Value = u.Username;

            var password = mgr.GetParameter();
            password.ParameterName = "@Password";
            password.Value = u.HashedPassword;

            return (LoginResult)mgr.Scalar("Login.usp_Login", username, password);
        }

        public LoginResult Register(User u)
        {
            Random r = new Random();
            u.Salt = r.NextLong(max: 0xFFFFFFFFFF).ToString("x").PadLeft(10, '0');

            var username = mgr.GetParameter();
            username.ParameterName = "@Username";
            username.Value = u.Username;

            var name = mgr.GetParameter();
            name.ParameterName = "@Name";
            name.Value = u.Name;

            var surname = mgr.GetParameter();
            surname.ParameterName = "@Surname";
            surname.Value = u.Surname;

            var email = mgr.GetParameter();
            email.ParameterName = "@Email";
            email.Value = u.Email;

            var password = mgr.GetParameter();
            password.ParameterName = "@Password";
            password.Value = u.HashedPassword;

            var salt = mgr.GetParameter();
            salt.ParameterName = "@Salt";
            salt.Value = u.Salt;

            return (LoginResult)mgr.Scalar("Login.usp_Register", username, name, surname, email, password, salt);
        }
    }

    public static class Extensions
    {
        public static long NextLong(this Random rand, long min = 0, long max = Int64.MaxValue)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }
    }
}