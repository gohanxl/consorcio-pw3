using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using ConsorcioPW3.App_Start;
using System.Web.Http;
using System.Web.Optimization;

namespace ConsorcioPW3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter
                    .SerializerSettings
                    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        protected void Application_PostAuthenticateRequest()
        {
            HttpCookie cookie = Request.Cookies["SESSION"];

            AuthenticationCookie(cookie);
        }

        protected void AuthenticationCookie(HttpCookie cookie)
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

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;

            int error = httpException != null ? httpException.GetHttpCode() : 0;

            Server.ClearError();
            Response.Redirect(String.Format("~/Error?error={0}", error, exception.Message));
        }
    }
}