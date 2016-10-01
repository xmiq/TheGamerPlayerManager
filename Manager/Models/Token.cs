using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Manager.Models
{
    public static class Token
    {
        public static Guid Value
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return Guid.Parse(FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
                }
                else
                {
                    return Guid.Empty;
                }
            }
        }
    }
}