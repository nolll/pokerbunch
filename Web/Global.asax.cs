using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.ApplicationInsights.Extensibility;
using Web.Plumbing;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;

            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();

            ViewEngines.Engines.Clear();
            var viewEngine = new CustomViewEngine();
            ViewEngines.Engines.Add(viewEngine);

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            SetupApplicationInsights();
        }

        private void SetupApplicationInsights()
        {
            if (SiteSettings.EnableApplicationInsights)
            {
                TelemetryConfiguration.Active.InstrumentationKey = SiteSettings.ApplicationInsightsKey;
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            EnsureLowercaseUrl(Context);
            RemoveWww(Context);
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            
        }

        protected void Application_End()
        {
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            RouteConfig.RegisterRoutes(routes);
        }

        private static void RemoveWww(HttpContext httpContext)
        {
            const string subdomainToRemove = "www";
            var stringToReplace = $"{subdomainToRemove}.";
            var uri = httpContext.Request.Url;
            var host = uri.Host;
            if (host.StartsWith(stringToReplace))
            {
                var hostWithoutWww = host.Replace(stringToReplace, string.Empty);
                var builder = new UriBuilder(uri) {Host = hostWithoutWww};
                RedirectPermanent(httpContext, builder.Uri.AbsoluteUri);
            }
        }

        private static void EnsureLowercaseUrl(HttpContext httpContext)
        {
            // Don't rewrite requests for content (.png, .css) or scripts (.js)
            if (IsExcluded(httpContext.Request.Url.AbsolutePath))
                return;

            // If uppercase chars exist, redirect to a lowercase version
            var url = httpContext.Request.Url.ToString();
            if (ContainsUpperCaseChars(url))
            {
                RedirectPermanent(httpContext, url.ToLower());
            }
        }

        private static bool ContainsUpperCaseChars(string url)
        {
            return Regex.IsMatch(url, @"[A-Z]");
        }

        private static void RedirectPermanent(HttpContext httpContext, string url)
        {
            httpContext.Response.Clear();
            httpContext.Response.Status = "301 Moved Permanently";
            httpContext.Response.StatusCode = (int)HttpStatusCode.MovedPermanently;
            httpContext.Response.AddHeader("Location", url);
            httpContext.Response.End();
        }

        private static bool IsExcluded(string url)
        {
            if (url.Contains("/Frontend/"))
                return true;
            return false;
        }
    }
}