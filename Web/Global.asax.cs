using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Application;
using Castle.MicroKernel;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core.Entities;
using Newtonsoft.Json;
using Plumbing;
using Web.Plumbing;
using Web.Security;
using DependencyResolver = Plumbing.DependencyResolver;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        private static DependencyResolver _dependencyResolver;

        protected void Application_Start()
        {
            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();

            AreaRegistration.RegisterAllAreas();

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BootstrapContainer();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            EnsureLowercaseUrl();
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                return;
            }

            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return;
            }
            
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
            {
                return;
            }

            var userIdentity = JsonConvert.DeserializeObject<UserIdentity>(authTicket.UserData);
            var customIdentity = new CustomIdentity(true, userIdentity);
            HttpContext.Current.User = new CustomPrincipal(customIdentity);
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

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            RouteConfig.RegisterRoutes(routes);
        }

        private static void BootstrapContainer()
        {
            var windsorContainer = new WindsorContainer().Install(FromAssembly.This());
            _dependencyResolver = new WebDependencyResolver(windsorContainer);
            var controllerFactory = new WindsorControllerFactory(windsorContainer.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_End()
        {
            _dependencyResolver.Dispose();
        }
    }
}