using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ControlSheet.Helper
{
    public class SecurityHelper
    {
        static public void GenerateAuthentication(string user)
        {
            var _user = new { Login = user };
            var _serializer = new JavaScriptSerializer();

            FormsAuthenticationTicket _authTicket = new FormsAuthenticationTicket(1,
                user,
                DateTime.Now,
                DateTime.Now.AddMinutes(Int16.Parse(ConfigurationManager.AppSettings["CookieExpireTime"])),
                true, _serializer.Serialize(_user));

            var _ticket = FormsAuthentication.Encrypt(_authTicket);

            FormsIdentity _formsIdentity = new FormsIdentity(_authTicket);

            HttpCookie _authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, _ticket);
            _authenticationCookie.Expires = _authTicket.Expiration;
            _authenticationCookie.Path = FormsAuthentication.FormsCookiePath;

            HttpContext.Current.Response.Cookies.Add(_authenticationCookie);

            var _httpAutCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (_httpAutCookie == null) return;

            HttpContext.Current.User = new GenericPrincipal(_formsIdentity, new string[] { });

            Thread.CurrentPrincipal = HttpContext.Current.User;
        }

        static public void LogOffUser()
        {
            FormsAuthentication.SetAuthCookie(string.Empty, false);
            HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            HttpCookie _authenticationCookie = new HttpCookie("UrbanChampion");
            _authenticationCookie.Expires = DateTime.Now.AddDays(-1D);
            _authenticationCookie.Path = FormsAuthentication.FormsCookiePath;

            HttpContext.Current.Response.Cookies.Add(_authenticationCookie);

            HttpCookie _authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            _authCookie.Expires = DateTime.Now.AddDays(-1D);
            _authCookie.Path = FormsAuthentication.FormsCookiePath;

            HttpContext.Current.Response.Cookies.Add(_authCookie);
            HttpContext.Current.Session["USER_SESSION_KEY"] = null;
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
    }
}