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

        public UserLogin Login(User u)
        {
            GetSalt(ref u);

            var username = mgr.GetParameter();
            username.ParameterName = "@Username";
            username.Value = u.Username;

            var password = mgr.GetParameter();
            password.ParameterName = "@Password";
            password.Value = u.HashPassword();

            return mgr.GetData("Login.usp_Login", username, password)
                .Select(x => new UserLogin
                {
                    Token = (!x.IsDBNull("Token")) ? Guid.Parse(x["Token"].ToString()) : Guid.Empty,
                    Result = (LoginResult)x["LoginResult"]
                })
                .FirstOrDefault();
        }

        public UserLogin Register(User u)
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
            password.Value = u.HashPassword();

            var salt = mgr.GetParameter();
            salt.ParameterName = "@Salt";
            salt.Value = u.Salt;

            return mgr.GetData("Login.usp_Register", username, name, surname, email, password, salt)
                .Select(x => new UserLogin
                {
                    Token = (!x.IsDBNull("Token")) ? Guid.Parse(x["Token"].ToString()) : Guid.Empty,
                    Result = (LoginResult)x["LoginResult"]
                })
                .FirstOrDefault();
        }

        public User GetUser(Guid Token)
        {
            var token = mgr.GetParameter();
            token.ParameterName = "@Token";
            token.Value = Token;

            return mgr.GetData("Login.usp_GetUserDetails", token)
                .Select(x => new User
                {
                    Username = x["Username"].ToString(),
                    Name = x["Name"].ToString(),
                    Surname = x["Surname"].ToString(),
                    Email = x["Email"].ToString()
                })
                .FirstOrDefault();
        }

        public void UpateUser(Guid Token, User u)
        {
            var token = mgr.GetParameter();
            token.ParameterName = "@Token";
            token.Value = Token;

            var name = mgr.GetParameter();
            name.ParameterName = "@Name";
            name.Value = u.Name;

            var surname = mgr.GetParameter();
            surname.ParameterName = "@Surname";
            surname.Value = u.Surname;

            var email = mgr.GetParameter();
            email.ParameterName = "@Email";
            email.Value = u.Email;

            mgr.GetData("Login.usp_UpdateUserDetails", token, name, surname, email);
        }

        public void ChangePassword(Guid Token, Password CurrentPassword, Password NextPassword)
        {
            var token = mgr.GetParameter();
            token.ParameterName = "@Token";
            token.Value = Token;

            var currentpassword = mgr.GetParameter();
            currentpassword.ParameterName = "@OldPassword";
            currentpassword.Value = CurrentPassword.Hash;

            var nextpassword = mgr.GetParameter();
            nextpassword.ParameterName = "@NewPassword";
            nextpassword.Value = NextPassword.Hash;

            mgr.GetData("Login.usp_UpdatePassword", token, currentpassword, nextpassword);
        }

        public bool IsOwner(Guid Token, string Username)
        {
            var token = mgr.GetParameter();
            token.ParameterName = "@Token";
            token.Value = Token;

            var username = mgr.GetParameter();
            username.ParameterName = "@Username";
            username.Value = Username;

            return Convert.ToBoolean(mgr.Scalar("Login.usp_IsOwner", token, username));
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

        public static bool IsDBNull(this System.Data.IDataRecord record, string name)
        {
            int ordinal = record.GetOrdinal(name);
            return record.IsDBNull(ordinal);
        }
    }
}