using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Core.Services;

namespace Web.Common
{
    public static class CommonWebFunctions
    {
        public static void EnsureHttps(HttpContext httpContext)
        {
            if (httpContext.Request.IsSecureConnection)
                return;

            if (Env.IsInProduction)
                httpContext.Response.RedirectPermanent(httpContext.Request.Url.ToString().Replace("http:", "https:"));
        }

        public static void EnsureLowercaseUrl(HttpContext httpContext)
        {
            // Don't rewrite requests for content (.png, .css) or scripts (.js)
            if (httpContext.Request.Url.AbsolutePath.Contains("/Frontend/"))
                return;

            // If uppercase chars exist, redirect to a lowercase version
            var url = httpContext.Request.Url.ToString();
            if (Regex.IsMatch(url, @"[A-Z]"))
            {
                httpContext.Response.Clear();
                httpContext.Response.Status = "301 Moved Permanently";
                httpContext.Response.StatusCode = (int)HttpStatusCode.MovedPermanently;
                httpContext.Response.AddHeader("Location", url.ToLower());
                httpContext.Response.End();
            }
        }
    }
}