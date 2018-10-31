using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Vue", "{*url}",
                new { controller = "Vue", action = "Root" }
            );
        }
    }
}