using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manager.Models
{
    public class UserLogin
    {
        public Guid Token { get; set; }

        public LoginResult Result { get; set; }
    }
}