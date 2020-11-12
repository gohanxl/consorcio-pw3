using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Optimization;
using ConsorcioPW3.App_Start;

namespace ConsorcioPW3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public void Application_PostAuthenticateRequest()
        {
            HttpCookie cookie = Request.Cookies["SESSION"];

            AuthenticationCookie(cookie);
        }

        public void AuthenticationCookie(HttpCookie cookie)
        {
            if (cookie != null)
            {
                String data = cookie.Value;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(data);

                String email = ticket.Name;
                String id = ticket.UserData;

                GenericIdentity identity = new GenericIdentity(email);
                GenericPrincipal user = new GenericPrincipal(identity, new String[] { id });

                HttpContext.Current.User = user;
            }
        }
    }
}