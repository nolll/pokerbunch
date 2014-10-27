using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Core;
using Core.Entities;
using Core.Services;
using Newtonsoft.Json;
using Web.Plumbing;
using Web.Security;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();

            ViewEngines.Engines.Clear();
            var viewEngine = new CustomViewEngine();
            ViewEngines.Engines.Add(viewEngine);

            AreaRegistration.RegisterAllAreas();

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            EnsureLowercaseUrl();
            EnsureHttps();
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
                return;

            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return;
            
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
                return;

            var userIdentity = JsonConvert.DeserializeObject<UserIdentity>(authTicket.UserData);
            var customIdentity = new CustomIdentity(true, userIdentity);
            HttpContext.Current.User = new CustomPrincipal(customIdentity);
        }

        protected void Application_End()
        {
        }

        private void EnsureLowercaseUrl()
        {
            // Don't rewrite requests for content (.png, .css) or scripts (.js)
            if (Request.Url.AbsolutePath.Contains("/Frontend/"))
                return;

            // If uppercase chars exist, redirect to a lowercase version
            var url = Request.Url.ToString();
            if (Regex.IsMatch(url, @"[A-Z]"))
            {
                Response.Clear();
                Response.Status = "301 Moved Permanently";
                Response.StatusCode = (int)HttpStatusCode.MovedPermanently;
                Response.AddHeader("Location", url.ToLower());
                Response.End();
            }
        }

        private void EnsureHttps()
        {
            if(Context.Request.IsSecureConnection)
                return;

            if(Env.IsInProduction)
                Response.RedirectPermanent(Context.Request.Url.ToString().Replace("http:", "https:"));
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            RouteConfig.RegisterRoutes(routes);
        }
    }
}