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

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Controller name", action = "Action name", id = UrlParameter.Optional}
                );

            routes.MapRoute("Error", "{*url}",
                new { controller = "Error", action = "NotFound" }
            );
        }
    }
}