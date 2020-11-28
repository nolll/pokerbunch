using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Bootstrapping;
using Web.Settings;

namespace Web
{
    public class Startup
    {
        private readonly AppSettings _settings;

        public Startup(IConfiguration configuration)
        {
            _settings = configuration.Get<AppSettings>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            new ServiceConfig(_settings, services).Configure();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            new AppConfig(app, env).Configure();
        }

        //private static void RemoveWww(HttpContext httpContext)
        //{
        //    const string subdomainToRemove = "www";
        //    var stringToReplace = $"{subdomainToRemove}.";
        //    var uri = httpContext.Request.Url;
        //    var host = uri.Host;
        //    if (host.StartsWith(stringToReplace))
        //    {
        //        var hostWithoutWww = host.Replace(stringToReplace, string.Empty);
        //        var builder = new UriBuilder(uri) { Host = hostWithoutWww };
        //        RedirectPermanent(httpContext, builder.Uri.AbsoluteUri);
        //    }
        //}

        //private static void EnsureLowercaseUrl(HttpContext httpContext)
        //{
        //    // Don't rewrite requests for content (.png, .css) or scripts (.js)
        //    if (IsExcluded(httpContext.Request.Url.AbsolutePath))
        //        return;

        //    // If uppercase chars exist, redirect to a lowercase version
        //    var url = httpContext.Request.Url.ToString();
        //    if (ContainsUpperCaseChars(url))
        //    {
        //        RedirectPermanent(httpContext, url.ToLower());
        //    }
        //}

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    if (filterContext.ExceptionHandled)
        //        return;

        //    if (filterContext.Exception is NotFoundException)
        //        HandleError(filterContext, 404, Error404);
        //    else if (filterContext.Exception is AccessDeniedException)
        //        HandleError(filterContext, 403, Error403);
        //    else if (filterContext.Exception is NotLoggedInException)
        //        HandleAuthCookieError(filterContext, 200, ErrorAuthCookie);
        //    else if (SiteSettings.HandleErrors)
        //        TrackAndHandleError(filterContext, 500, Error500);
        //}

        //private void ClearAllCookies(ExceptionContext filterContext)
        //{
        //    for (var i = 0; i < filterContext.HttpContext.Request.Cookies.Count; i++)
        //    {
        //        var cookie = filterContext.HttpContext.Request.Cookies[i];
        //        if (cookie == null) continue;
        //        var cookieName = cookie.Name;
        //        var clearedCookie = new HttpCookie(cookieName) { Expires = DateTime.Now.AddDays(-1) };
        //        filterContext.HttpContext.Response.Cookies.Add(clearedCookie);
        //    }
        //}

        //public class CustomAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
        //{
        //    protected override bool AuthorizeCore(HttpContextBase httpContext)
        //    {
        //        var isAuthorized = base.AuthorizeCore(httpContext);
        //        if (!isAuthorized)
        //            return false;
        //        var identity = new UserIdentity(httpContext.User);
        //        if (!identity.IsAuthenticated)
        //            return false;
        //        return true;
        //    }
        //}
    }
}
