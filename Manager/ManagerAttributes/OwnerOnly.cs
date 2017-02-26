using Manager.DataManagement;
using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manager.ManagerAttributes
{
    public class OwnerOnly : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                return false;
            }

            var rd = httpContext.Request.RequestContext.RouteData;

            string username = (rd.Values.ContainsKey("username")) ? rd.Values["username"].ToString() : (rd.Values["id"] ?? 0).ToString();

            //Sometimes username is not recognized in RouteData
            if (username.All(x => Char.IsNumber(x)))
            {
                username = httpContext.Request.Params["username"].ToString().Split(',').FirstOrDefault();
            }

            UserManager mgr = new UserManager();

            return mgr.IsOwner(Token.Value, username);
        }
    }
}